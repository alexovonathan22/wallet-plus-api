using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletPlusApi.Core.DTOs
{
    public class LoginResponseDTO
    {
        public string AuthToken { get; set; }
        public string RefreshToken { get; set; }
        public string Email { get; set; }
    }
}
