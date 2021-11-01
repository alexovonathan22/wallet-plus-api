using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletPlusApi.Core.DTOs
{
    public class SendMoneyDTO
    {
        [Required]
        public decimal AmountToAdd { get; set; }
        [Required]
        public string BeneficiaryWalletId { get; set; } // Same as account number 
        [Required]
        public string DepositingWalletId { get; set; }
        // 1 - for 3rd party service, 2 - for Customer owned wallet. by default 
        //a cst would use 1 to topup wallet; then when topup for another wallet will choose 1 or 2
        [Required]
        public int TopUpMode { get; set; }
    }
}
