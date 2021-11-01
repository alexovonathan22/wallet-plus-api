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
        public decimal AmountAdded { get; set; }
        public bool IsWithdrawal { get; set; } // false is deposit true is withdrawal
        public string Status { get; set; }
        public string TransRef { get; set; }
        public string TopUpMode { get; set; }
        public string Narration { get; set; }
        public long BeneficiaryId { get; set; }
        public long DepositorId { get; set; } // when adding money if money is to self, both ids are the same.
        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
