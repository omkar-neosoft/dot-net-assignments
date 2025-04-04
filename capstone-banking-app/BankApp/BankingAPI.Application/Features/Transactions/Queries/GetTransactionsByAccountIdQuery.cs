using System;
using System.Collections.Generic;
using BankingAPI.Domain.Models;
using MediatR;

namespace BankingAPI.Application.Features.Transactions.Queries {
    public class GetTransactionsByAccountIdQuery : IRequest<IEnumerable<Transaction>> {
        public Guid AccountId {
            get; set;
        }
    }
}
