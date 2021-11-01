using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletPlusApi.Core.DTOs;

namespace WalletPlusApi.Infrastructure.Services.Interfaces
{
    public interface IMock3RDPartyService
    {
        /// <summary>
        /// Is mocking a sitution where the customer 
        /// adds money via paystack, flutterwave etc
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ThirdPartyResponseDTO CompleteAddMoney(AddMoneyDTO model);
        public ThirdPartyResponseDTO CompleteSendMoney(SendMoneyDTO model);
    }
}
