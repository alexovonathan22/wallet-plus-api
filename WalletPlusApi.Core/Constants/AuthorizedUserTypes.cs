using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletPlusApi.Core.Constants
{
    public static class AuthorizedUserTypes
    {
        public const string Admin = "AuthorizedAdmin";
        public const string Customer = "AuthorizedCustomer";
        public const string UserAndAdmin = "AuthorizedUserAdmin";

    }

    public static class Roles
    {
        // Active
        public const string Admin = "Administrator";
        public const string Customer = "Customer";
    }

    public static class NumericConstants
    {
        // Active
        public const int StringLength = 10;
    }
}
