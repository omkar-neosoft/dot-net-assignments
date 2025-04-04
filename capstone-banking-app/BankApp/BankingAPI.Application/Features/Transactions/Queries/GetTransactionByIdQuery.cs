using System;
using BankingAPI.Domain.Models;
using MediatR;

namespace BankingAPI.Application.Features.Transactions.Queries {
    public class GetTransactionByIdQuery : IRequest<Transaction> {
        public Guid Id {
            get; set;
        }
    }
}
