using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletPlusApi.Core.DTOs;

namespace WalletPlusApi.Infrastructure.Services.Interfaces
{
    public interface IBillerService
    {
        public BillerResponseDTO BillerDstv(SpendMoneyDTO model, string BillerMode);
        public BillerResponseDTO BillerAirtime(SpendMoneyDTO model, string BillerMode);

    }
}
