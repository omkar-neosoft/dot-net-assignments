namespace BankingAPI.Application.Interfaces {
    public interface IUnitOfWork {
        IAccountRepository Accounts {
            get;
        }
        ITransactionRepository Transactions {
            get;
        }
        Task<int> CompleteAsync();
    }
}
