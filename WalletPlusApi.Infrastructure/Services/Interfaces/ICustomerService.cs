using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletPlusApi.Core.Common.Responses;
using WalletPlusApi.Core.DTOs;

namespace WalletPlusApi.Infrastructure.Services.Interfaces
{
    public interface ICustomerService 
    {
        Task<BaseResponse> SignUp(CustomerDTO reqModel);
        Task<BaseResponse> Login(LoginDTO reqModel);
    }
}
