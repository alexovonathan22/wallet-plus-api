using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletPlusApi.Core.Common.Responses;
using WalletPlusApi.Core.Data;
using WalletPlusApi.Core.DTOs;

namespace WalletPlusApi.Infrastructure.Services.Interfaces
{
    public interface IWalletService
    {
        Task<(PointWallet createdWallet, string message)> CreateWallet(CustomerDTO reqModel);
        /// <summary>
        /// To add money for self and to receive money
        /// </summary>
        /// <param name="reqModel"></param>
        /// <returns></returns>
        Task<BaseResponse> AddMoney(AddMoneyDTO reqModel); 
        /// <summary>
        /// To send money to other wallet holders
        /// </summary>
        /// <param name="reqModel"></param>
        /// <returns></returns>
        Task<BaseResponse> SendMoney(SendMoneyDTO reqModel);
        /// <summary>
        /// To get transactions that have impacted a customer wallet
        /// </summary>
        /// <returns></returns>
        Task<BaseResponse> GetWalletTransactions(int skip, int take); // remember to paginate
        
        // depending on query params passed either to get amountBal or PointBal or Both
        Task<BaseResponse> GetWalletBalances(bool GetMoneyBalance, bool GetPointBalance); 
        Task<BaseResponse> SpendMoney(SpendMoneyDTO reqModel);

    }
}
