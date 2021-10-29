using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WalletPlusApi.Core.Util
{
    public static class UtilMethods
    {
        public static string GenerateRandomString(int strLength)
        {
            if (strLength < 2)
            {
                return string.Empty;
            }
            Random rand = new Random();
            string digits = rand.Next(0, 999999).ToString($"D{strLength}");
            return digits;
        }

        public static string GenerateSecretkey()
        {
            var key = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
                generator.GetBytes(key);
            string apiKey = Convert.ToBase64String(key);
            return apiKey;
        }
    }
}
