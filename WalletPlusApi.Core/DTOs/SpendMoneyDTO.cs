using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletPlusApi.Core.DTOs
{
    public class SpendMoneyDTO
    {
        [Required]
        public string WalletIdToDebit { get; set; } // if customer wallet is to be debited else use 3rd party mode
        [Required]
        public decimal AmountToBePaid { get; set; }
        [Required]
        public int BillerService { get; set; } // airtime or dstv
        [Required]
        public int PaymentMode { get; set; } // wallet or 3rd party
    }
}
