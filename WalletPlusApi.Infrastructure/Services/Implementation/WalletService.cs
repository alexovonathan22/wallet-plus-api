using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletPlusApi.Core.Common.Responses;
using WalletPlusApi.Core.Constants;
using WalletPlusApi.Core.Data;
using WalletPlusApi.Core.DTOs;
using WalletPlusApi.Core.Util;
using WalletPlusApi.Infrastructure.Persistence;
using WalletPlusApi.Infrastructure.Services.Interfaces;

namespace WalletPlusApi.Infrastructure.Services.Implementation
{
    public class WalletService : IWalletService
    {
        #region Fields
        private readonly IRepository<PointWallet> _ptwalletrepo;
        private readonly IRepository<Customer> _cstrepo;

        #endregion
        public WalletService(IRepository<PointWallet> ptwalletrepo, IRepository<Customer> cstrepo)
        {
            _ptwalletrepo = ptwalletrepo;
            _cstrepo = cstrepo;
        }

        public Task<BaseResponse> AddMoney(AddMoneyDTO reqModel)
        {
            throw new NotImplementedException();
        }

        public async Task<(PointWallet createdWallet, string message)> CreateWallet(CustomerDTO reqModel)
        {
            var CheckCustomer = await _cstrepo.Get(t => t.Email == reqModel.Email);
            if(CheckCustomer == null)
            {
                return (createdWallet: null, message: $"Something went wrong.");
            }
            var newWallet = new PointWallet
            {
                CreatedAt = DateTime.Now,
                LastUpdated = DateTime.Now,
                CustomerId = CheckCustomer.Id,
                WalletId = UtilMethods.GenerateRandomString(NumericConstants.StringLength),
                BalanceAmount=0.00m,
                PointEarned=0,
                MoneyValueForPointEarned =0.00m
            };
            try
            {
                await _ptwalletrepo.Add(newWallet);
                var (id, IsSaved) = await _ptwalletrepo.Save();

                if (IsSaved)
                {
                    return (newWallet, $"Wallet created {newWallet.WalletId}.");
                }

            }catch(Exception e)
            {
                return (null, $"Wallet created Unsuccessfully.");
            }
            return (null, $"Wallet created Unsuccessfully.");

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
