using System;
using BankingAPI.Domain.Models;
using MediatR;

namespace BankingAPI.Application.Features.Accounts.Queries {
    public class GetAccountByIdQuery : IRequest<Account> {
        public Guid Id {
            get; set;
        }
    }
}
