using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingAPI.Application.Interfaces;
using BankingAPI.Domain.Models;
using BankingAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Infrastructure.Repositories {
    public class TransactionRepository : ITransactionRepository {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<Transaction> GetByIdAsync(Guid id) {
            return await _context.Transactions.FindAsync(id);
        }

        public async Task<IEnumerable<Transaction>> GetByAccountIdAsync(Guid accountId) {
            return await _context.Transactions.Where(t => t.AccountId == accountId).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetByUserIdAsync(string userId) {
            return await _context.Transactions
                .Include(t => t.Account)
                .Where(t => t.Account.UserId == userId) // Ensure UserId exists in Account
                .ToListAsync();
        }

        public async Task CreateAsync(Transaction transaction) {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id) {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null) {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateTransactionAsync(Guid fromAccountId, Guid toAccountId, double amount, TransactionType Type) {
            var transaction = new Transaction {
                FromAccountId = fromAccountId,
                AccountId = toAccountId,
                Amount = amount,
                Date = DateTime.UtcNow,
                Type = Type,
            };

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
