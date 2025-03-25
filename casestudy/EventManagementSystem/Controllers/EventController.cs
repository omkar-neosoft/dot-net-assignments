using System.Threading.Tasks;
using EventManagementSystem.Models;
using EventManagementSystem.Services;
using EventManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystem.Controllers {
    [Authorize]
    public class EventController : Controller {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService) {
            _eventService = eventService;
        }

        public async Task<IActionResult> Index() {
            var events = await _eventService.GetAllEventsAsync();
            if (events == null || !events.Any()) {
                TempData["ErrorMessage"] = "No events found.";
            }
            return View(events);
        }

        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Create() {
            //ViewBag.Categories = _eventService.GetAllCategoriesAsync().Result;
            var categories = await _eventService.GetAllCategoriesAsync();
            var organizers = await _eventService.GetAllOrganizersAsync();
            ViewBag.Categories = categories.Select(c => new SelectListItem {
                Value = c.Id.ToString(),
                Text = c.Name
            });

            ViewBag.Organizers = organizers.Select(o => new SelectListItem {
                Value = o.Id.ToString(),
                Text = o.FirstName + " " + o.LastName
            });

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Create(EventViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            await _eventService.AddEventAsync(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id) {
            var eventEntity = await _eventService.GetEventByIdAsync(id);
            if (eventEntity == null) {
                return NotFound();
            }
            return View(eventEntity);
        }

        public async Task<IActionResult> GetEventDetails(int id) {
            var eventEntity = await _eventService.GetEventByIdAsync(id);
            if (eventEntity == null) {
                return NotFound();
            }

            return PartialView("_EventDetailsModal", eventEntity);
        }

        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Edit(int id) {
            var eventEntity = await _eventService.GetEventByIdAsync(id);
            if (eventEntity == null) {
                return NotFound();
            }

            var model = new EventViewModel {
                Id = eventEntity.Id,
                Name = eventEntity.Name,
                CategoryId = eventEntity.CategoryId,
                OrganizerId = eventEntity.OrganizerId,
                Location = eventEntity.Location,
                ImagePath = eventEntity.ImagePath,
                Date = eventEntity?.Date ?? System.DateTime.Now,
                AvailableSeats = eventEntity?.AvailableSeats ?? 0,
                TicketPrice = eventEntity?.TicketPrice ?? 0
            };

            //var eventViewModel = await _eventService.GetEventByIdAsync(id);
            //if (eventViewModel == null)
            //    return NotFound();

            var categories = await _eventService.GetAllCategoriesAsync();
            var organizers = await _eventService.GetAllOrganizersAsync();

            ViewBag.Categories = categories.Select(c => new SelectListItem {
                Value = c.Id.ToString(),
                Text = c.Name
            });

            ViewBag.Organizers = organizers.Select(o => new SelectListItem {
                Value = o.Id.ToString(),
                Text = o.FirstName + " " + o.LastName
            });

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Edit(EventViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            await _eventService.UpdateEventAsync(model);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Delete(int id) {
            await _eventService.DeleteEventAsync(id);
            return RedirectToAction("Index");
        }
    }
}




//using EventManagementSystem.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace EventManagementSystem.Controllers {
//    [Authorize(Roles = "Admin,Organizer")]
//    public class EventController : Controller {
//        public IActionResult Create() {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Create(Event model) {
//            if (ModelState.IsValid) {
//                // Add event logic
//                return RedirectToAction("Index");
//            }
//            return View(model);
//        }
//    }
//}
