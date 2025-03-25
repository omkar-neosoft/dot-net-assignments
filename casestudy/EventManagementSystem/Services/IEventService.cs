using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagementSystem.Models;
using EventManagementSystem.ViewModels;

namespace EventManagementSystem.Services {
    public interface IEventService {
        Task<IEnumerable<EventViewModel>> GetAllEventsAsync();
        Task<EventViewModel?> GetEventByIdAsync(int id);
        Task AddEventAsync(EventViewModel eventViewModel);
        Task UpdateEventAsync(EventViewModel eventViewModel);
        Task DeleteEventAsync(int id);

        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<IEnumerable<ApplicationUser>> GetAllOrganizersAsync();
    }
}
