using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletPlusApi.Core.Data;
using WalletPlusApi.Infrastructure.Persistence;

namespace WalletPlusApi.Infrastructure.Services.Util
{
    public static class ServiceUtil
    {
        public async static Task<Customer> GetCustomerDetails(HttpContext ctx, IRepository<Customer> cstrepo)
        {
            if (ctx.Request == null || cstrepo == null) return null;
            var secKey = CheckPubKey(ctx);
            var merchant = await cstrepo.Get(r => r.IsActive && !r.IsDeleted && r.SecretKey == secKey);
            if (merchant != null) return merchant;
            return null;
        }

        public static string CheckPubKey(HttpContext httpContext)
        {
            //request validation
            var httpRequest = httpContext.Request;

            if (!httpRequest.Headers.TryGetValue("Authorization", out StringValues auth))
            {
                throw new Exception("The secret key must be provided in the Authorization header.");
            }

            var pubkey = auth.First();
            if (!pubkey.StartsWith("Bearer WPlus-"))
            {
                throw new Exception("The secret key is not valid.");
            }

            var secretKey = pubkey.Replace("Bearer ", "");
            return secretKey;
        }
    }
}
