using System.Diagnostics;
using EventManagementSystem.Models;
using EventManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementSystem.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEventService _eventService;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, IEventService eventService) {
            _logger = logger;
            _userManager = userManager;
            _eventService = eventService;
        }

        public async Task<IActionResult> Index() {
            var userId = _userManager.GetUserId(User);
            var featuredEvents = await _eventService.GetAllEventsAsync();
            ViewBag.FeaturedEvents = featuredEvents;

            if (userId != null) {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [Authorize]
        public IActionResult Dashboard() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Organizers() {
            var organizers = await _userManager.GetUsersInRoleAsync("Organizer");
            return View(organizers.Where(u => !u.IsDeleted).ToList());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteOrganizer(string userId) {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound("User not found.");

            user.IsDeleted = true; // Soft delete
            await _userManager.UpdateAsync(user);

            return RedirectToAction("Organizers");
        }

        [Route("Home/AccessDenied")]
        public IActionResult AccessDenied() {
            var userId = _userManager.GetUserId(User);
            if (userId != null) {
                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("Index");
        }
    }
}
