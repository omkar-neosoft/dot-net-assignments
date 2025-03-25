using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagementSystem.Models;

namespace EventManagementSystem.Repositories {
    public interface ITicketBookingRepository {
        Task<IEnumerable<TicketBooking>> GetUserBookingsAsync(string userId);
        Task<IEnumerable<TicketBooking>> GetAllUserBookingsAsync(string userId);
        Task<bool> BookTicketAsync(TicketBooking booking);
        Task<TicketBooking?> GetBookingDetailsAsync(int bookingId);
        Task<bool> CancelBookingAsync(int bookingId);
    }
}
