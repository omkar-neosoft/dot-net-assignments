using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingAPI.Application.Interfaces;
using BankingAPI.Domain;
using MediatR;

namespace BankingAPI.Application.Features.BankFeature.Query.GetBankById {
    public class GetBankByIdQueryHandler : IRequestHandler<GetBankByIdQuery, Bank> {
        readonly IBankRepository _BankRepository;
        public GetBankByIdQueryHandler(IBankRepository BankRepository) {
            _BankRepository = BankRepository;

        }
        public async Task<Bank> Handle(GetBankByIdQuery request, CancellationToken cancellationToken) {
            var BankFindStatus = await _BankRepository.GetBankByIdAsync(request.id);
            return BankFindStatus;
        }
    }
}
