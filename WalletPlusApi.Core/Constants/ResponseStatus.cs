using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletPlusApi.Core.Constants
{
    public static class ResponseStatus
    {
        public static (string Message, string Code) Success { get; set; } = ("success", "00");
        public static (string Message, string Code) Fail { get; set; } = ("fail", "00");

    }
}
