using BankingAPI.Domain;
using BankingAPI.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAPI.Infrastructure.Context
{
   public class BankDbContext:DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext>options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BankConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Author> Authors{ get; set; }

    }
}
