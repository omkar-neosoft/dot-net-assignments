using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagementSystem.Models;
using EventManagementSystem.Repositories;
using EventManagementSystem.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace EventManagementSystem.Services {
    public class EventService : IEventService {
        private readonly IEventRepository _eventRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EventService(IEventRepository eventRepository, IWebHostEnvironment webHostEnvironment) {
            _eventRepository = eventRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<EventViewModel>> GetAllEventsAsync() {
            var events = await _eventRepository.GetAllEventsAsync();
            return events.Select(e => new EventViewModel {
                Id = e.Id,
                Name = e.Name,
                OrganizerId = e.OrganizerId,
                ImagePath = e.ImagePath,
                //OrganizerName = e.Organizer?.Name ?? "Unknown",
                CategoryId = e.CategoryId,
                //CategoryName = e.Category?.Name ?? "Unknown",
                Location = e.EventDetails?.Location ?? "",
                Date = e.EventDetails?.Date ?? System.DateTime.MinValue,
                AvailableSeats = e.EventDetails?.AvailableSeats ?? 0,
                TicketPrice = e.EventDetails?.TicketPrice ?? 0
            });
        }

        public async Task<EventViewModel?> GetEventByIdAsync(int id) {
            var e = await _eventRepository.GetEventByIdAsync(id);
            if (e == null)
                return null;

            return new EventViewModel {
                Id = e.Id,
                Name = e.Name,
                OrganizerId = e.OrganizerId,
                //OrganizerName = e.Organizer?.Name ?? "Unknown",
                CategoryId = e.CategoryId,
                ImagePath = e.ImagePath,
                //CategoryName = e.Category?.Name ?? "Unknown",
                Location = e.EventDetails?.Location ?? "",
                Date = e.EventDetails?.Date ?? System.DateTime.MinValue,
                AvailableSeats = e.EventDetails?.AvailableSeats ?? 0,
                TicketPrice = e.EventDetails?.TicketPrice ?? 0
            };
        }

        private async Task<string> SaveImageAsync(IFormFile imageFile, string name) {
            //string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, $"images/{name?.Trim().ToLower().Replace(" ", "_")}");
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            //string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName?.Trim().ToLower().Replace(" ", "_");
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create)) {
                await imageFile.CopyToAsync(fileStream);
            }

            return "/images/" + uniqueFileName;
        }

        public async Task AddEventAsync(EventViewModel eventViewModel) {
            string imagePath = "/images/default-event.jpg"; // Default image path
            if (eventViewModel.ImageFile != null) {
                imagePath = await SaveImageAsync(eventViewModel.ImageFile, eventViewModel.Name);
            }

            var eventEntity = new Event {
                Name = eventViewModel.Name,
                OrganizerId = eventViewModel.OrganizerId,
                CategoryId = eventViewModel.CategoryId,
                ImagePath = imagePath,
                EventDetails = new EventDetails {
                    Location = eventViewModel.Location,
                    Date = eventViewModel.Date,
                    AvailableSeats = eventViewModel.AvailableSeats,
                    TicketPrice = eventViewModel.TicketPrice
                }
            };

            await _eventRepository.AddEventAsync(eventEntity);
        }

        public async Task UpdateEventAsync(EventViewModel eventViewModel) {
            var existingEvent = await _eventRepository.GetEventByIdAsync(eventViewModel.Id);
            if (existingEvent == null)
                return;

            existingEvent.Name = eventViewModel.Name;
            existingEvent.OrganizerId = eventViewModel.OrganizerId;
            existingEvent.CategoryId = eventViewModel.CategoryId;

            if (eventViewModel.ImageFile != null) {
                existingEvent.ImagePath = await SaveImageAsync(eventViewModel.ImageFile, eventViewModel.Name);
            }

            if (existingEvent.EventDetails != null) {
                existingEvent.EventDetails.Location = eventViewModel.Location;
                existingEvent.EventDetails.Date = eventViewModel.Date;
                existingEvent.EventDetails.AvailableSeats = eventViewModel.AvailableSeats;
                existingEvent.EventDetails.TicketPrice = eventViewModel.TicketPrice;
            }

            await _eventRepository.UpdateEventAsync(existingEvent);
        }

        public async Task DeleteEventAsync(int id) {
            await _eventRepository.DeleteEventAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync() {
            return await _eventRepository.GetAllCategoriesAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllOrganizersAsync() {
            return await _eventRepository.GetAllOrganizersAsync();
        }
    }
}
