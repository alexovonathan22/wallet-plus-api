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
        public const string BaseUrl = Version + "api/[controller]";

        public static class User
        {
            #region Post
            public const string NewUser = BaseUrl + "/signup";
            
            #endregion

            #region Get
            public const string NewMerchant = BaseUrl + "/";
            #endregion
            public const string TestWebHook = BaseUrl + "/notify";
        }

        public static class Wallet
        {
            #region Post
            public const string GetMerchant = BaseUrl + "/{id}/profile";

            #endregion
            #region Get
            public const string Rt = BaseUrl + "/{id}/profile";

            #endregion
        }

        public static class Update
        {

        }

        public static class Delete
        {

        }
    }
}
