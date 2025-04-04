using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingAPI.Application.Interfaces;
using BankingAPI.Domain;
using MediatR;

namespace BankingAPI.Application.Features.BankFeature.Query.GetAllBanks {
    public class GetAllBanksQueryHandler : IRequestHandler<GetAllBanksQuery, IEnumerable<Bank>> {
        readonly IBankRepository _BankRepository;
        //constructor
        public GetAllBanksQueryHandler(IBankRepository BankRepository) {
            _BankRepository = BankRepository;
        }
        public async Task<IEnumerable<Bank>> Handle(GetAllBanksQuery request, CancellationToken cancellationToken) {
            var allBanks = await _BankRepository.GetBanks();
            return allBanks;
        }
    }
}
