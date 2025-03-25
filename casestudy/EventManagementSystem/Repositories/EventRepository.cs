using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagementSystem.Data;
using EventManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystem.Repositories {
    public class EventRepository : IEventRepository {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EventRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager) {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync() {
            return await _context.Events
                                 .Where(e => !e.IsDeleted) // Exclude soft-deleted events
                                 .Include(e => e.EventDetails)
                                 .Include(e => e.Organizer)
                                 .Include(e => e.Category)
                                 .ToListAsync();
        }

        public async Task<Event?> GetEventByIdAsync(int id) {
            return await _context.Events
                                 .Where(e => !e.IsDeleted) // Exclude soft-deleted events
                                 .Include(e => e.EventDetails)
                                 .Include(e => e.Organizer)
                                 .Include(e => e.Category)
                                 .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddEventAsync(Event eventEntity) {
            _context.Events.Add(eventEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEventAsync(Event eventEntity) {
            var existingEvent = await _context.Events.FindAsync(eventEntity.Id);
            if (existingEvent != null && !existingEvent.IsDeleted) {
                _context.Events.Update(eventEntity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteEventAsync(int id) {
            var eventEntity = await _context.Events.FindAsync(id);
            if (eventEntity != null) {
                eventEntity.IsDeleted = true; // Soft delete
                _context.Events.Update(eventEntity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync() {
            // Get all Categories
            return await _context.Categories.ToListAsync();
        }

        //public async Task<IEnumerable<Organizer>> GetAllOrganizersAsync() {
        //    return await _context.Organizers.ToListAsync();
        //}

        public async Task<IEnumerable<ApplicationUser>> GetAllOrganizersAsync() {
            var users = await _userManager.GetUsersInRoleAsync("Organizer");
            return users.Where(u => !u.IsDeleted).ToList();
        }
    }
}
