using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletPlusApi.Core.Constants
{
    public enum TopUpMode
    {
        ThirdParty = 1,
        CustomerWallet 
    }

    public enum Billers
    {
        Dstv = 1,
        Airtime
    }
}
