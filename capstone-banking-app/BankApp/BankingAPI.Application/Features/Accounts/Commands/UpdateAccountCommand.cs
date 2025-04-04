using BankingAPI.Domain.Models;
using MediatR;

namespace BankingAPI.Application.Features.Accounts.Commands {
    public class UpdateAccountCommand : IRequest<bool> {
        public Guid Id {
            get; set;
        }
        public double Balance {
            get; set;
        }
    }
}
