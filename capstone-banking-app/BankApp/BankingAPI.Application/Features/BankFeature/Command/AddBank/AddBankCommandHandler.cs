using BankingAPI.Application.Interfaces;
using BankingAPI.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAPI.Application.Features.BankFeature.Command.AddBank
{
    public class AddBankCommandHandler : IRequestHandler<AddBankCommand, Bank>
    {
        readonly IBankRepository _BankRepository;
        public AddBankCommandHandler(IBankRepository BankRepository)
        {
            _BankRepository = BankRepository;

        }
        public Task<Bank> Handle(AddBankCommand request, CancellationToken cancellationToken)
        {
            var Bank = _BankRepository.AddBankAsync(request.Bank);
            return Bank;
        }
    }
}
