using System.Threading.Tasks;
using BankingAPI.Application.Interfaces;
using BankingAPI.Infrastructure.Context;

namespace BankingAPI.Infrastructure.Repositories {
    public class UnitOfWork : IUnitOfWork {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context) {
            _context = context;
            Accounts = new AccountRepository(_context);
            Transactions = new TransactionRepository(_context);
        }

        public IAccountRepository Accounts {
            get;
        }
        public ITransactionRepository Transactions {
            get;
        }

        public async Task<int> CompleteAsync() {
            return await _context.SaveChangesAsync();
        }
    }
}
