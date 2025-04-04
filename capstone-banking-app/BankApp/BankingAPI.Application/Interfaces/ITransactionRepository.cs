using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankingAPI.Domain.Models;

namespace BankingAPI.Application.Interfaces {
    public interface ITransactionRepository {
        Task<Transaction> GetByIdAsync(Guid id);
        Task<IEnumerable<Transaction>> GetByAccountIdAsync(Guid accountId);
        Task<IEnumerable<Transaction>> GetByUserIdAsync(string userId);
        Task CreateAsync(Transaction transaction);
        Task DeleteAsync(Guid id);
        Task CreateTransactionAsync(Guid fromAccountId, Guid toAccountId, double amount, TransactionType Type);
    }
}
