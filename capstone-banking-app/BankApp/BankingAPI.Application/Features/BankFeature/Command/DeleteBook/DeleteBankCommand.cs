using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAPI.Application.Features.BankFeature.Command.DeleteBank
{
    public record DeleteBankCommand(int id):IRequest<bool>;
    
}
