using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingAPI.Domain;
using MediatR;

namespace BankingAPI.Application.Features.BankFeature.Query.GetAllBanks {
    public record GetAllBanksQuery() : IRequest<IEnumerable<Bank>>;



}
