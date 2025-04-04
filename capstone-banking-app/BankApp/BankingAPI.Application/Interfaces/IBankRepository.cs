using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingAPI.Domain;

namespace BankingAPI.Application.Interfaces {
    public interface IBankRepository {
        Task<IEnumerable<Bank>> GetBanks();
        Task<Bank> GetBankByIdAsync(int id);
        Task<Bank> AddBankAsync(Bank Bank);
        Task<Bank> UpdateBankAsync(int BankId, Bank Bank);
        Task<bool> DeleteBankAsync(int id);


    }
}
