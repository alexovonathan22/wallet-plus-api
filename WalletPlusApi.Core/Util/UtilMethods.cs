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
        public static decimal CalculatePointsEarned (decimal amt)
        {
            if (amt < 0) return 0;
            var pointEarned = 0m;
           if((amt >= 5000 && amt <= 10000))
           {
              pointEarned = 0.01m * amt;
           }

            if ((amt >= 10001 && amt <= 25000))
            {
                pointEarned = 0.025m * amt;
            }

            if (amt >= 25001)
            {
                pointEarned = 0.05m * amt;
            }
            if (amt < 5000)
            {
                pointEarned = 0;
            }

            return pointEarned;
        }

        public static bool CalculateIfValueGreaterThanOnemillion(decimal walletAmt, decimal amountTobeAdded)
        {
            if (walletAmt <= 0 || amountTobeAdded <= 0) return false;
            return (walletAmt += amountTobeAdded) > 1000000;
        }
    }
}
