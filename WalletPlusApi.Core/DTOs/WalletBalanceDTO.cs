using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletPlusApi.Core.DTOs
{
    public class WalletBalanceDTO
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public int PointEarned { get; set; }
        public decimal Balance { get; set; }
        public string WalletId { get; set; }
    }
}
