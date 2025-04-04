using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using BankingAPI.Application.Exceptions;
using BankingAPI.Application.Features.Accounts.Commands;
using BankingAPI.Application.Features.Accounts.Queries;
using BankingAPI.Application.Interfaces;
using BankingAPI.Domain.Models;
using MediatR;

namespace BankingAPI.Application.Features.Accounts {
    public class AccountHandler :
        IRequestHandler<CreateAccountCommand, Account>,
        IRequestHandler<UpdateAccountCommand, bool>,
        IRequestHandler<DeleteAccountCommand, bool>,
        IRequestHandler<GetAccountByIdQuery, Account>,
        IRequestHandler<GetAccountsByUserIdQuery, IEnumerable<Account>> {
        private readonly IUnitOfWork _unitOfWork;

        public AccountHandler(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public async Task<Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken) {
            var existingAccounts = await _unitOfWork.Accounts.GetByUserIdAsync(request.UserId);

            if (existingAccounts.Any(a => a.AccountType == request.AccountType)) {
                throw new BadRequestException("You already have this type of account.");
            }
            var account = new Account {
                UserId = request.UserId,
                Balance = request.Balance,
                AccountType = request.AccountType
            };

            await _unitOfWork.Accounts.CreateAsync(account);
            return account;
        }

        public async Task<bool> Handle(UpdateAccountCommand request, CancellationToken cancellationToken) {
            var account = await _unitOfWork.Accounts.GetByIdAsync(request.Id);
            if (account == null)
                return false;

            account.Balance = request.Balance;
            await _unitOfWork.Accounts.UpdateAsync(account);
            return true;
        }

        public async Task<bool> Handle(DeleteAccountCommand request, CancellationToken cancellationToken) {
            await _unitOfWork.Accounts.DeleteAsync(request.Id);
            return true;
        }

        public async Task<Account> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken) {
            return await _unitOfWork.Accounts.GetByIdAsync(request.Id);
        }

        public async Task<IEnumerable<Account>> Handle(GetAccountsByUserIdQuery request, CancellationToken cancellationToken) {
            return await _unitOfWork.Accounts.GetByUserIdAsync(request.UserId);
        }
    }
}
