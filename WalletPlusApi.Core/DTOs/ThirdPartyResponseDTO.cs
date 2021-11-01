using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletPlusApi.Core.DTOs
{
    public class ThirdPartyResponseDTO
    {
        public bool Status  { get; set; }
        public string StatusCode { get; set; }
        public decimal Amount { get; set; }
        public string Message { get; set; }
    }
}
