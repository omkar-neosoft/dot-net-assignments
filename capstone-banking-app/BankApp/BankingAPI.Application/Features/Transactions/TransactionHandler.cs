using BankingAPI.Application.Features.Transactions.Commands;
using BankingAPI.Application.Features.Transactions.Queries;
using BankingAPI.Application.Interfaces;
using BankingAPI.Domain.Models;
using MediatR;

namespace BankingAPI.Application.Features.Transactions {
    public class TransactionHandler :
        IRequestHandler<CreateTransactionCommand, Transaction>,
        IRequestHandler<DeleteTransactionCommand, bool>,
        IRequestHandler<GetTransactionByIdQuery, Transaction>,
        IRequestHandler<GetTransactionsByAccountIdQuery, IEnumerable<Transaction>>,
        IRequestHandler<GetTransactionsByUserIdQuery, IEnumerable<Transaction>> {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionHandler(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public async Task<Transaction> Handle(CreateTransactionCommand request, CancellationToken cancellationToken) {
            if (request.Type == TransactionType.Transfer && request.AccountId == null) {
                throw new Exception("ToAccountId is required for transfers.");
            }

            var transaction = new Transaction {
                FromAccountId = request.Type == TransactionType.Transfer ? request.FromAccountId : null,
                AccountId = request.AccountId,
                Type = request.Type,
                Amount = request.Amount,
                Description = request.Description,
                Date = DateTime.UtcNow
            };

            await _unitOfWork.Transactions.CreateAsync(transaction);

            // Fetch the sender account
            var fromAccount = await _unitOfWork.Accounts.GetByIdAsync(request.AccountId);
            if (fromAccount == null) {
                throw new Exception("Sender account not found.");
            }

            // If it's a Deposit, update the recipient account
            if (request.Type == TransactionType.Deposit && request.AccountId != null) {
                fromAccount.Balance += request.Amount;
                await _unitOfWork.Accounts.UpdateAsync(fromAccount);
            }

            // Ensure sufficient balance for withdrawal/transfer
            if ((request.Type == TransactionType.Withdrawal || request.Type == TransactionType.Transfer) && fromAccount.Balance < request.Amount) {
                throw new Exception("Insufficient balance.");
            }

            if (request.Type == TransactionType.Withdrawal) {
                fromAccount.Balance -= request.Amount;
                await _unitOfWork.Accounts.UpdateAsync(fromAccount);
            }

            bool isTraansactionSuccess = false;
            // If it's a transfer, update the recipient account
            if (request.Type == TransactionType.Transfer && request.FromAccountId != null) {
                var toAccount = await _unitOfWork.Accounts.GetByIdAsync(request.FromAccountId);
                if (toAccount == null) {
                    throw new Exception("Recipient account not found.");
                }

                toAccount.Balance += request.Amount;
                await _unitOfWork.Accounts.UpdateAsync(toAccount);
                isTraansactionSuccess = true;

                var newtransaction = new Transaction {
                    FromAccountId = request.AccountId,
                    AccountId = request.FromAccountId,
                    Type = request.Type,
                    Amount = request.Amount,
                    Description = request.Description,
                    Date = DateTime.UtcNow
                };

                await _unitOfWork.Transactions.CreateAsync(newtransaction);
            }

            // Update sender's balance
            if (request.Type == TransactionType.Transfer && isTraansactionSuccess) {
                fromAccount.Balance -= request.Amount;
                await _unitOfWork.Accounts.UpdateAsync(fromAccount);
            }

            return transaction;
        }


        public async Task<bool> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken) {
            await _unitOfWork.Transactions.DeleteAsync(request.Id);
            return true;
        }

        public async Task<Transaction> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken) {
            return await _unitOfWork.Transactions.GetByIdAsync(request.Id);
        }

        public async Task<IEnumerable<Transaction>> Handle(GetTransactionsByAccountIdQuery request, CancellationToken cancellationToken) {
            return await _unitOfWork.Transactions.GetByAccountIdAsync(request.AccountId);
        }

        public async Task<IEnumerable<Transaction>> Handle(GetTransactionsByUserIdQuery request, CancellationToken cancellationToken) {
            return await _unitOfWork.Transactions.GetByUserIdAsync(request.UserId);
        }
    }
}
