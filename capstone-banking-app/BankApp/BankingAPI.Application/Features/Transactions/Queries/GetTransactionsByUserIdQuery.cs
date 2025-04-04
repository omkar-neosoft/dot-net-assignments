using System.Collections.Generic;
using BankingAPI.Domain.Models;
using MediatR;

namespace BankingAPI.Application.Features.Transactions.Queries {
    public class GetTransactionsByUserIdQuery : IRequest<IEnumerable<Transaction>> {
        public string UserId {
            get; set;
        }
    }
}
