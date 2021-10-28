using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletPlusApi.Core.Common.Responses;
using WalletPlusApi.Core.DTOs;
using WalletPlusApi.Infrastructure.Services.Interfaces;

namespace WalletPlusApi.Infrastructure.Services.Implementation
{
    public class WalletService : IWalletService
    {
        #region Fields
        #endregion
        public WalletService()
        {

        }

        public Task<BaseResponse> AddMoney(AddMoneyDTO reqModel)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> CreateWallet(CustomerDTO reqModel)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> GetWalletBalances(bool GetMoneyBalance, bool GetPointBalance)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> GetWalletTransactions()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> SendMoney(SendMoneyDTO reqModel)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> SpendMoney(SpendMoneyDTO reqModel)
        {
            throw new NotImplementedException();
        }
    }
}
