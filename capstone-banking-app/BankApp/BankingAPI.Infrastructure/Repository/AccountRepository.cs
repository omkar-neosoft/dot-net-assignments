using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankingAPI.Application.Interfaces;
using BankingAPI.Domain.Models;
using BankingAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Infrastructure.Repositories {
    public class AccountRepository : IAccountRepository {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<Account> GetByIdAsync(Guid accountId) {
            return await _context.Accounts
                .Include(a => a.Transactions)
                .FirstOrDefaultAsync(a => a.Id == accountId);
        }

        public async Task<IEnumerable<Account>> GetByUserIdAsync(string userId) {
            return await _context.Accounts
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }

        public async Task CreateAsync(Account account) {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Account account) {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid accountId) {
            var account = await _context.Accounts.FindAsync(accountId);
            if (account != null) {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Account> GetByAccountIdAsync(Guid accountId) {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
        }
    }
}
