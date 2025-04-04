using MediatR;

namespace BankingAPI.Application.Features.Accounts.Commands {
    public class DeleteAccountCommand : IRequest<bool> {
        public Guid Id {
            get; set;
        }
    }
}
