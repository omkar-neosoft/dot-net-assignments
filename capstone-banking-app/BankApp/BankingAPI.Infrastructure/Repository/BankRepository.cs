using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingAPI.Application.Interfaces;
using BankingAPI.Domain;
using BankingAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Infrastructure.Repository {
    public class BankRepository : IBankRepository {
        protected readonly BankDbContext _BankDbContext;
        //constructor injection
        public BankRepository(BankDbContext BankDbContext) {
            _BankDbContext = BankDbContext;
        }
        public async Task<Bank> AddBankAsync(Bank Bank) {
            await _BankDbContext.Banks.AddAsync(Bank);
            await _BankDbContext.SaveChangesAsync();
            return Bank;
        }

        public async Task<bool> DeleteBankAsync(int id) {
            var Bank = await GetBankByIdAsync(id);
            if (Bank is not null) {
                _BankDbContext.Banks.Remove(Bank);
                return await _BankDbContext.SaveChangesAsync() > 0;

            }
            return false;

        }

        public async Task<Bank> GetBankByIdAsync(int id) {
            return await _BankDbContext.Banks.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Bank>> GetBanks() {
            return await _BankDbContext.Banks.ToListAsync();
        }

        public Task<Bank> UpdateBankAsync(int BankId, Bank Bank) {
            throw new NotImplementedException();
        }
    }
}
