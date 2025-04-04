using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingAPI.Application.Exceptions;
using BankingAPI.Application.Interfaces;
using MediatR;

namespace BankingAPI.Application.Features.BankFeature.Command.DeleteBank {
    public class DeleteBankCommandhandler : IRequestHandler<DeleteBankCommand, bool> {
        readonly IBankRepository _BankRepository;
        public DeleteBankCommandhandler(IBankRepository BankRepository) {
            _BankRepository = BankRepository;

        }
        public async Task<bool> Handle(DeleteBankCommand request, CancellationToken cancellationToken) {
            var BankFindStatus = await _BankRepository.GetBankByIdAsync(request.id);
            if (BankFindStatus is null) {
                throw new NotFoundException($"Bank with Id::{request.id} not found");

            }
            return await _BankRepository.DeleteBankAsync(BankFindStatus.Id);

        }
    }
}
