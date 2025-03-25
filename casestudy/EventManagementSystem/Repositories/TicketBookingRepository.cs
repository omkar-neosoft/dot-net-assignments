using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagementSystem.Data;
using EventManagementSystem.Models;
using EventManagementSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystem.Repositories {
    public class TicketBookingRepository : ITicketBookingRepository {
        private readonly ApplicationDbContext _context;

        public TicketBookingRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<TicketBooking>> GetUserBookingsAsync(string userId) {
            return await _context
                .TicketBookings.Where(tb =>
                    tb.UserId == userId && !tb.IsDeleted && tb.Status == "Confirmed"
                )
                .Include(tb => tb.Event)
                .ThenInclude(e => e.EventDetails)
                .Include(b => b.Event)
                .ThenInclude(e => e.Organizer) // Load Organizer (ApplicationUser)
                .ToListAsync();
        }

        public async Task<IEnumerable<TicketBooking>> GetAllUserBookingsAsync(string userId) {
            return await _context
                .TicketBookings.Where(tb =>
                    !tb.IsDeleted && tb.Status == "Confirmed"
                )
                .Include(tb => tb.Event)
                .ThenInclude(e => e.EventDetails)
                .Include(b => b.Event)
                .ThenInclude(e => e.Organizer)
                .Include(b => b.User)
                .ToListAsync();
        }

        public async Task<bool> BookTicketAsync(TicketBooking booking) {
            _context.TicketBookings.Add(booking);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<TicketBooking?> GetBookingDetailsAsync(int bookingId) {
            return await _context
                .TicketBookings.Include(tb => tb.Event)
                .ThenInclude(e => e.EventDetails)
                .FirstOrDefaultAsync(tb => tb.Id == bookingId);
        }

        public async Task<bool> CancelBookingAsync(int bookingId) {
            var booking = await _context.TicketBookings.FindAsync(bookingId);
            if (booking == null) {
                return false;
            }

            booking.Status = "Cancelled";
            booking.IsDeleted = true;

            //_context.TicketBookings.Remove(booking);
            _context.TicketBookings.Update(booking);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
