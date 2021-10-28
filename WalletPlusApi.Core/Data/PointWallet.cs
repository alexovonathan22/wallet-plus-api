using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletPlusApi.Core.Data
{
    public class PointWallet : MoneyWallet
    {
        public int PointEarned { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal MoneyValueForPointEarned { get; set; }
    }
}
