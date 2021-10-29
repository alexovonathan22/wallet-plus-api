using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletPlusApi.Core.Common.RouteDeclarations
{
    public static class ApiRoutes
    {

        public const string Version = "v1";
        public const string BaseUrl = "api/[controller]";

        public static class User
        {
            #region Post
            public const string NewUser = BaseUrl + "/signup";
            public const string Login = BaseUrl + "/login";
            #endregion
        }

        public static class Wallet
        {
            #region
            public const string AddMoney = BaseUrl + "/add_money";
            public const string SendMoney = BaseUrl + "/send_money";
            public const string GetWalletTransactions = BaseUrl + "/transaction";
            public const string GetWalletBalances = BaseUrl + "/balance";
            public const string SpendMoney = BaseUrl + "/spend_money";

            #endregion
        }
    }
}
