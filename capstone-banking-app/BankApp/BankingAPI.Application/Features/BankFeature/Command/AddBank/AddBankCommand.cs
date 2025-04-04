using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingAPI.Domain;
using MediatR;

namespace BankingAPI.Application.Features.BankFeature.Command.AddBank {
    public record AddBankCommand(Bank Bank) : IRequest<Bank>;

}
