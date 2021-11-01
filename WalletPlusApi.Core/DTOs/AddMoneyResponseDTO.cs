using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletPlusApi.Core.DTOs
{
    public class AddMoneyResponseDTO
    {

        public decimal AmountToAdd { get; set; }
        public string BeneficiaryWalletId { get; set; } // Same as account number
        public string DepositingWalletId { get; set; } // Same as account number
        public string TopUpMode { get; set; } // "card", "ussd"
        public string Message { get; set; }
    }
}
