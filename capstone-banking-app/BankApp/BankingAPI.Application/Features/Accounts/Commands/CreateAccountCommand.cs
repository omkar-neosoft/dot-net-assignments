using BankingAPI.Domain.Models;
using MediatR;

namespace BankingAPI.Application.Features.Accounts.Commands {
    public class CreateAccountCommand : IRequest<Account> {
        public string UserId {
            get; set;
        }
        public double Balance {
            get; set;
        }
        public AccountType AccountType {
            get; set;
        }
    }
}
