using System;
using BankingAPI.Domain.Models;
using MediatR;

namespace BankingAPI.Application.Features.Transactions.Commands {
    public class CreateTransactionCommand : IRequest<Transaction> {
        public Guid AccountId {
            get; set;
        }

        public Guid FromAccountId {
            get; set;
        }
        public TransactionType Type {
            get; set;
        }
        public double Amount {
            get; set;
        }
        public string Description {
            get; set;
        }
    }
}
