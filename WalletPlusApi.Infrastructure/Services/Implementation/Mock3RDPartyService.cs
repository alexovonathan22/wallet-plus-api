using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletPlusApi.Core.DTOs;
using WalletPlusApi.Infrastructure.Services.Interfaces;

namespace WalletPlusApi.Infrastructure.Services.Implementation
{
    public class Mock3RDPartyService : IMock3RDPartyService
    {
        public ThirdPartyResponseDTO CompleteAddMoney(AddMoneyDTO model)
        {
            if (model == null)
            {
                return new ThirdPartyResponseDTO
                {
                    Message = $"Payment of {model.AmountToAdd} unsuccessful.",
                    StatusCode = "01"
                };
            }

            return new ThirdPartyResponseDTO
            {
                Message = $"Payment of {model.AmountToAdd} paid.",
                Status=true,
                StatusCode="00",
                Amount=model.AmountToAdd
            };
        }

        public ThirdPartyResponseDTO CompleteSendMoney(SendMoneyDTO model)
        {
            if (model == null)
            {
                return new ThirdPartyResponseDTO
                {
                    Message = $"Payment of {model.AmountToAdd} unsuccessful.",
                    StatusCode = "01"
                };
            }

            return new ThirdPartyResponseDTO
            {
                Message = $"Payment of {model.AmountToAdd} paid.",
                Status = true,
                StatusCode = "00",
                Amount = model.AmountToAdd
            };
        }
    }
}
