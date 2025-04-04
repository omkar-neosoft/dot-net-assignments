namespace BankingAPI.Domain.Models {
    public class Account {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId {
            get; set;
        } // Stores the reference to Identity User
        public double Balance {
            get; set;
        }
        public AccountType AccountType {
            get; set;
        }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }

    public enum AccountType {
        Savings,
        Current,
    }
}
