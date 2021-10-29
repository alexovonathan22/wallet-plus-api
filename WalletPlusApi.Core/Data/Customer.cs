using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletPlusApi.Core.Common;

namespace WalletPlusApi.Core.Data
{
    public class Customer : BaseEntity
    {
        [StringLength(70)]
        public string FirstName { get; set; }
        [StringLength(70)]
        public string LastName { get; set; }
        [StringLength(70)]
        public string Email { get; set; }
        [StringLength(70)]
        public string PhoneNumber { get; set; }
        public string OTP { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string AuthToken { get; set; }
        public string RefreshToken { get; set; }
        public string SecretKey { get; set; }
        public string EncryptionKey { get; set; } // if time available send enc key n secret key to email.
        
        public virtual MoneyWallet CustomerWallet { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

    }
}
