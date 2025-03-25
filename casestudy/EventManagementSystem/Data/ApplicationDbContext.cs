using EventManagementSystem.Models;
using EventManagementSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystem.Data {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }

        public DbSet<Event> Events {
            get; set;
        }
        public DbSet<EventDetails> EventDetails {
            get; set;
        }
        public DbSet<TicketBooking> TicketBookings {
            get; set;
        }
        public DbSet<Category> Categories {
            get; set;
        }
        public DbSet<Payment> Payments {
            get; set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.EventDetails)
                .WithOne(ed => ed.Event)
                .HasForeignKey<EventDetails>(ed => ed.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Event>()
            .HasOne(e => e.Organizer)
            .WithMany()
            .HasForeignKey(e => e.OrganizerId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Category)
                .WithMany()
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TicketBooking>()
                .HasOne(tb => tb.Event)
                .WithMany(e => e.TicketBookings)
                .HasForeignKey(tb => tb.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TicketBooking>()
                .HasOne(tb => tb.User)
                .WithMany(u => u.TicketBookings)
                .HasForeignKey(tb => tb.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TicketBooking>()
                .HasOne(tb => tb.Payment)
                .WithOne(p => p.TicketBooking)
                .HasForeignKey<Payment>(p => p.TicketBookingId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Music", IsDeleted = false },
                new Category { Id = 2, Name = "Sports", IsDeleted = false },
                new Category { Id = 3, Name = "Technology", IsDeleted = false },
                new Category { Id = 4, Name = "Art & Culture", IsDeleted = false },
                new Category { Id = 5, Name = "Health & Wellness", IsDeleted = false }
            );
        }
        public DbSet<EventManagementSystem.ViewModels.TicketBookingViewModel> TicketBookingViewModel { get; set; } = default!;
    }
}
