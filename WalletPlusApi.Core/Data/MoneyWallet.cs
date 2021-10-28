using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletPlusApi.Core.Common;

namespace WalletPlusApi.Core.Data
{
    public class MoneyWallet : BaseEntity
    {
        // when a transaction occurs withdraw or deposit the amount is updated accordingly
        [Column(TypeName = "decimal(18,2)")]
        public decimal BalanceAmount { get; set; }
        //WalletId same as account number
        public string WalletId { get; set; } 
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
