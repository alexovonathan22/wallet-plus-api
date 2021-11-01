using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletPlusApi.Core.Data;

namespace WalletPlusApi.Core.DTOs
{
    public class TransactionResponseDTO
    {
        public decimal AmountAdded { get; set; }
        public bool IsWithdrawal { get; set; } // false is deposit true is withdrawal
        public string Status { get; set; }
        public string TransRef { get; set; }
        public string TopUpMode { get; set; }
        public long BeneficiaryId { get; set; }
        public long DepositorId { get; set; } // when adding money if money is to self, both ids are the same.
        public long CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }


        public List<TransactionResponseDTO> CreateTxnMap(List<Transaction> txn)
        {
            if (txn == null) return null;
            var txnDTO = new List<TransactionResponseDTO>();
            txn.ForEach(r =>
               txnDTO.Add(new TransactionResponseDTO()
               {
                   TransRef = r.TransRef,
                   TopUpMode=r.TopUpMode,
                   AmountAdded=r.AmountAdded,
                   BeneficiaryId=r.BeneficiaryId,
                   DepositorId=r.DepositorId,
                   CustomerId=r.CustomerId,
                   CreatedAt=r.CreatedAt,
                   IsWithdrawal=r.IsWithdrawal,
                   Status=r.Status
               })
            );
            return txnDTO;
        }
    }
}
