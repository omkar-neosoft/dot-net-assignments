using BankingAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Infrastructure.Context {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Account> Accounts {
            get; set;
        }
        public DbSet<Transaction> Transactions {
            get; set;
        }
        //public DbSet<RefreshToken> RefreshTokens {
        //    get; set;
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            // Define relationships
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Transactions)
                .WithOne(t => t.Account)
                .HasForeignKey(t => t.AccountId);

        }
    }
}
