using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagementSystem.Models;
using EventManagementSystem.ViewModels;

namespace EventManagementSystem.Services {
    public interface ITicketBookingService {
        Task<IEnumerable<TicketBookingViewModel>> GetUserBookingsAsync(string userId);
        Task<IEnumerable<TicketBookingViewModel>> GetAllUserBookingsAsync(string userId);
        Task<bool> BookTicketAsync(TicketBooking bookingModel, string userId);
        Task<TicketBookingViewModel?> GetBookingDetailsAsync(int bookingId);
        Task<bool> CancelBookingAsync(int bookingId);
    }
}
