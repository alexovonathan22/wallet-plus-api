using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletPlusApi.Core.DTOs
{
    public class BillerResponseDTO
    {
        public string Biller { get; set; }
        public decimal AmountPaidForService { get; set; }
        public string Narration { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }

    }
}
