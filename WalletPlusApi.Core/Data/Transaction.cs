using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletPlusApi.Core.Common;

namespace WalletPlusApi.Core.Data
{
    public class Transaction : BaseEntity
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Withdrawal { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Deposit { get; set; }
        public long BeneficiaryId { get; set; }
        public long DepositorId { get; set; } // when adding money if money is to self, both ids are the same.
    }
}
