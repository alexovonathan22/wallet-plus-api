using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletPlusApi.Core.Common.Responses;
using WalletPlusApi.Core.DTOs;

namespace WalletPlusApi.Infrastructure.Services.Interfaces
{
    public interface IWalletService
    {
        Task<BaseResponse> CreateWallet(CustomerDTO reqModel);
        Task<BaseResponse> AddMoney(AddMoneyDTO reqModel);
        Task<BaseResponse> SendMoney(SendMoneyDTO reqModel);
        Task<BaseResponse> GetWalletTransactions(); // remember to paginate
        
        // depending on query params passed either to get amountBal or PointBal or Both
        Task<BaseResponse> GetWalletBalances(bool GetMoneyBalance, bool GetPointBalance); 
        Task<BaseResponse> SpendMoney(SpendMoneyDTO reqModel);

    }
}
