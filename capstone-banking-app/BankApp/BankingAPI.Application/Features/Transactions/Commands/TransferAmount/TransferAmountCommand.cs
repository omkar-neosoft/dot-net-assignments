using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingAPI.Domain.Models;
using MediatR;

namespace BankingAPI.Application.Features.Transactions.Commands.TransferAmount {
    public class TransferAmountCommand : IRequest<bool> {
        public Guid FromAccountNumber {
            get; set;
        }
        public Guid ToAccountNumber {
            get; set;
        }
        public double Amount {
            get; set;
        }
        public TransactionType Type {
            get; set;
        }

        public TransferAmountCommand(Guid fromAccountNumber, Guid toAccountNumber, double amount) {
            FromAccountNumber = fromAccountNumber;
            ToAccountNumber = toAccountNumber;
            Amount = amount;
        }
    }
}
