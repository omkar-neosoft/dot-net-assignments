using System.Collections.Generic;
using BankingAPI.Domain.Models;
using MediatR;

namespace BankingAPI.Application.Features.Accounts.Queries {
    public class GetAccountsByUserIdQuery : IRequest<IEnumerable<Account>> {
        public string UserId {
            get; set;
        }
    }
}
