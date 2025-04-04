using BankingAPI.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingAPI.Infrastructure.Configuration
{
    internal class BankConfiguration : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> builder)
        {
            builder.HasData(
                new Bank
                {
                    Id = 1,
                    Name="Bank1",
                    Price=1200,
                    AuthorId=1
                });

        }
    }
}