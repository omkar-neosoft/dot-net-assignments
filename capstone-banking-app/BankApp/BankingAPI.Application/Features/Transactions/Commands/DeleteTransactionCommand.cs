using System;
using MediatR;

namespace BankingAPI.Application.Features.Transactions.Commands {
    public class DeleteTransactionCommand : IRequest<bool> {
        public Guid Id {
            get; set;
        }
    }
}
