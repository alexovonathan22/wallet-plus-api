using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletPlusApi.Core.DTOs;
using WalletPlusApi.Infrastructure.Services.Interfaces;

namespace WalletPlusApi.Infrastructure.Services.Implementation
{
    public class BillerService : IBillerService
    {
       
        public BillerResponseDTO BillerAirtime(SpendMoneyDTO model, string BillerMode)
        {
            if (model == null)
            {
                return new BillerResponseDTO
                {
                    Message = $"Payment of {model.AmountToBePaid} unsuccessful for {BillerMode}",
                    AmountPaidForService = model.AmountToBePaid,
                    Status = false,
                    Biller = "Mtn",
                    Narration="Airtime purchase failed.",
                };
            }

            return new BillerResponseDTO
            {
                Message = $"Payment of {model.AmountToBePaid} paid succesfully.",
                Status = true,
                AmountPaidForService = model.AmountToBePaid,
                Biller = BillerMode,
                Narration="Airtime purchase success."    
            };

        }

        public BillerResponseDTO BillerDstv(SpendMoneyDTO model, string BillerMode)
        {
            if (model == null)
            {
                return new BillerResponseDTO
                {
                    Message = $"Payment of USD {model.AmountToBePaid} unsuccessful for {BillerMode}",
                    AmountPaidForService = model.AmountToBePaid,
                    Status = false,
                    Biller = BillerMode,
                    Narration = $"{BillerMode} purchase failed.",
                };
            }

            return new BillerResponseDTO
            {
                Message = $"Payment of USD {model.AmountToBePaid} paid succesfully.",
                Status = true,
                AmountPaidForService = model.AmountToBePaid,
                Biller = BillerMode,
                Narration = $"{BillerMode} purchase success."
            };
        }
    }
}
