using BankingAPI.Domain.Models;

namespace BankingAPI.Application.Interfaces {
    public interface IAccountRepository {
        Task<Account> GetByIdAsync(Guid accountId);
        Task<IEnumerable<Account>> GetByUserIdAsync(string userId);
        Task CreateAsync(Account account);
        Task UpdateAsync(Account account);
        Task DeleteAsync(Guid accountId);

        Task<Account> GetByAccountIdAsync(Guid accountId);
    }
}
