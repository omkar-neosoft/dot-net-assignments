using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagementSystem.Models;
using EventManagementSystem.Repositories;
using EventManagementSystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystem.Services {
    public class TicketBookingService : ITicketBookingService {
        private readonly ITicketBookingRepository _ticketBookingRepository;
        private readonly IEventRepository _eventRepository;

        public TicketBookingService(ITicketBookingRepository ticketBookingRepository, IEventRepository eventRepository) {
            _ticketBookingRepository = ticketBookingRepository;
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<TicketBookingViewModel>> GetUserBookingsAsync(string userId) {
            var bookings = await _ticketBookingRepository.GetUserBookingsAsync(userId);

            return bookings.Select(b => new TicketBookingViewModel {
                Id = b.Id,
                UserId = b.UserId,
                EventId = b.Event?.Id ?? 0,
                EventName = b.Event?.Name ?? "Unknown Event",
                EventLocation = b.Event?.EventDetails?.Location ?? "Unknown Location",
                EventDate = b.Event?.EventDetails?.Date ?? DateTime.MinValue,
                OrganizerName = b.Event?.Organizer != null
                        ? $"{b.Event.Organizer.FirstName} {b.Event.Organizer.LastName}"
                        : "Unknown Organizer",
                Quantity = b.Quantity,
                BookingDate = b.BookingDate
            });
        }
        public async Task<IEnumerable<TicketBookingViewModel>> GetAllUserBookingsAsync(string userId) {
            var bookings = await _ticketBookingRepository.GetAllUserBookingsAsync(userId);

            return bookings.Select(b => new TicketBookingViewModel {
                Id = b.Id,
                UserId = b.UserId,
                UserName = b.User != null ? $"{b.User?.FirstName ?? "Unknown First Name"} {b.User?.LastName ?? "Unknown Last Name"}" : "Unknown User",
                EventId = b.Event?.Id ?? 0,
                EventName = b.Event?.Name ?? "Unknown Event",
                EventLocation = b.Event?.EventDetails?.Location ?? "Unknown Location",
                EventDate = b.Event?.EventDetails?.Date ?? DateTime.MinValue,
                OrganizerName = b.Event?.Organizer != null
                        ? $"{b.Event.Organizer.FirstName} {b.Event.Organizer.LastName}"
                        : "Unknown Organizer",
                Quantity = b.Quantity,
                BookingDate = b.BookingDate
            });
        }

        public async Task<bool> BookTicketAsync(TicketBooking bookingModel, string userId) {
            var eventEntity = await _eventRepository.GetEventByIdAsync(bookingModel.EventId);
            if (eventEntity == null || eventEntity.EventDetails.AvailableSeats < bookingModel.Quantity) {
                return false;
            }

            bookingModel.UserId = userId;
            bookingModel.Status = "Confirmed";

            bool isBooked = await _ticketBookingRepository.BookTicketAsync(bookingModel);
            if (isBooked) {
                eventEntity.EventDetails.AvailableSeats -= bookingModel.Quantity;
                await _eventRepository.UpdateEventAsync(eventEntity);
            }

            return isBooked;
        }

        public async Task<TicketBookingViewModel?> GetBookingDetailsAsync(int bookingId) {
            var booking = await _ticketBookingRepository.GetBookingDetailsAsync(bookingId);
            if (booking == null) {
                return null;
            }

            return new TicketBookingViewModel {
                Id = booking.Id,
                EventId = booking.EventId,
                UserId = booking.UserId,
                Quantity = booking.Quantity,
                BookingDate = booking.BookingDate
            };
        }

        public async Task<bool> CancelBookingAsync(int bookingId) {
            var booking = await _ticketBookingRepository.GetBookingDetailsAsync(bookingId);
            if (booking == null) {
                return false;
            }

            var eventEntity = await _eventRepository.GetEventByIdAsync(booking.EventId);
            if (eventEntity != null) {
                eventEntity.EventDetails.AvailableSeats += booking.Quantity;
                await _eventRepository.UpdateEventAsync(eventEntity);
            }

            return await _ticketBookingRepository.CancelBookingAsync(bookingId);
        }
    }
}
