using BankingAPI.Application.Interfaces;
using MediatR;

namespace BankingAPI.Application.Features.Transactions.Commands.TransferAmount {
    public class TransferAmountCommandHandler : IRequestHandler<TransferAmountCommand, bool> {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransferAmountCommandHandler(
            IAccountRepository accountRepository,
            ITransactionRepository transactionRepository
        ) {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<bool> Handle(
            TransferAmountCommand request,
            CancellationToken cancellationToken
        ) {
            var fromAccount = await _accountRepository.GetByAccountIdAsync(
                request.FromAccountNumber
            );
            var toAccount = await _accountRepository.GetByAccountIdAsync(
                request.ToAccountNumber
            );

            if (fromAccount == null || toAccount == null)
                throw new Exception("Invalid account details.");

            if (fromAccount.Balance < request.Amount)
                throw new Exception("Insufficient balance.");

            fromAccount.Balance -= request.Amount;
            toAccount.Balance += request.Amount;

            await _accountRepository.UpdateAsync(fromAccount);
            await _accountRepository.UpdateAsync(toAccount);

            // Log the transaction
            //await _transactionRepository.CreateTransactionAsync(fromAccount.Id, toAccount.Id, request.Amount);
            await _transactionRepository.CreateTransactionAsync(
                fromAccount.Id,
                toAccount.Id,
                request.Amount,
                request.Type
            );

            return true;
        }
    }
}
