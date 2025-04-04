namespace BankingAPI.Domain.Models {
    public class Transaction {
        public Guid Id { get; set; } = Guid.NewGuid();

        // Nullable for deposits & withdrawals
        public Guid? FromAccountId {
            get; set;
        }
        public Account? FromAccount {
            get; set;
        }

        //// Nullable for withdrawals
        //public Guid? ToAccountId {
        //    get; set;
        //}
        //public Account? ToAccount {
        //    get; set;
        //}

        public Guid AccountId {
            get; set;
        }
        public Account Account {
            get; set;
        }
        public TransactionType Type {
            get; set;
        }
        public double Amount {
            get; set;
        }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Description {
            get; set;
        }
    }

    public enum TransactionType {
        Deposit,
        Withdrawal,
        Transfer,
    }
}
