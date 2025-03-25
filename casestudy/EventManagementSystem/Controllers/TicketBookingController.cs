using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using EventManagementSystem.Models;
using EventManagementSystem.Services;
using EventManagementSystem.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mono.TextTemplating;

namespace EventManagementSystem.Controllers {
    [Authorize] // Only logged-in users can book tickets
    public class TicketBookingController : Controller {
        private readonly ITicketBookingService _ticketBookingService;
        private readonly IEventService _eventService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketBookingController(ITicketBookingService ticketBookingService, IEventService eventService, UserManager<ApplicationUser> userManager) {
            _ticketBookingService = ticketBookingService;
            _eventService = eventService;
            _userManager = userManager;
        }

        // GET: /TicketBooking/MyBookings
        public async Task<IActionResult> MyBookings() {
            var userId = _userManager.GetUserId(User);
            if (User.IsInRole("Admin")) {
                var bookings = await _ticketBookingService.GetAllUserBookingsAsync(userId);
                return View(bookings);
            } else {
                var bookings = await _ticketBookingService.GetUserBookingsAsync(userId);
                return View(bookings);
            }
        }

        // GET: /TicketBooking/Book/{eventId}
        [HttpGet]
        public async Task<IActionResult> Book(int id) {
            var eventId = id;
            var eventEntity = await _eventService.GetEventByIdAsync(eventId);
            if (eventEntity == null) {
                return NotFound();
            }

            ViewBag.eventEntity = eventEntity;

            var userId = _userManager.GetUserId(User); // Get logged-in user's ID

            var bookingModel = new TicketBooking {
                EventId = eventId,
                UserId = userId,
                Status = "Confirmed"
            };

            return View(bookingModel);
        }

        // POST: /TicketBooking/Book
        [HttpPost]
        //public async Task<IActionResult> Book(TicketBookingViewModel bookingViewModel) {
        public async Task<IActionResult> BookN(int Quantity, int EventId) {
            //if (!ModelState.IsValid) {
            //    return View(bookingModel);
            //}
            // var eventId = Quantity;
            //return View();
            var quantity = Quantity;
            var userId = _userManager.GetUserId(User); // Get logged-in user's ID

            var bookingModel = new TicketBooking {
                EventId = EventId,
                UserId = userId,
                Quantity = quantity,
                Status = "Confirmed"
            };

            //var userId = _userManager.GetUserId(User);
            var success = await _ticketBookingService.BookTicketAsync(bookingModel, userId);

            if (!success) {
                TempData["Message"] = "Failed to book ticket.Ensure there are enough seats available.";
                TempData["MessageType"] = "error";
                ModelState.AddModelError("", "Failed to book ticket. Ensure there are enough seats available.");
                TempData.Keep();
                return View(bookingModel);
            }

            TempData["Message"] = "Your ticket has been booked successfully!";
            TempData["MessageType"] = "success";
            TempData.Keep();

            return RedirectToAction(nameof(MyBookings));
        }

        // GET: /TicketBooking/Cancel/{bookingId}
        public async Task<IActionResult> Cancel(int bookingId) {
            var booking = await _ticketBookingService.GetBookingDetailsAsync(bookingId);
            if (booking == null || booking.UserId != _userManager.GetUserId(User)) {
                return NotFound();
            }
            return View(booking);
        }

        // POST: /TicketBooking/Cancel/{bookingId}
        //[HttpPost, ActionName("Cancel")]
        [HttpPost]
        public async Task<IActionResult> CancelConfirmed(int Id) {
            var success = await _ticketBookingService.CancelBookingAsync(Id);

            if (!success) {
                return NotFound();
            }

            return RedirectToAction(nameof(MyBookings));
        }
    }
}
