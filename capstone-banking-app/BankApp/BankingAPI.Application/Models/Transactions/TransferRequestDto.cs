using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAPI.Application.Models.Transactions {
    public class TransferRequestDto {
        public string FromAccountNumber {
            get; set;
        }
        public string ToAccountNumber {
            get; set;
        }
        public double Amount {
            get; set;
        }
    }

}
