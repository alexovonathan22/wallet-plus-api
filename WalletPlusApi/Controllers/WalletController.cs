using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletPlusApi.Core.Common.RouteDeclarations;
using WalletPlusApi.Core.DTOs;
using WalletPlusApi.Infrastructure.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WalletPlusApi.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }
        [HttpPost(ApiRoutes.Wallet.AddMoney)]
        public async Task<IActionResult> AddMoney(AddMoneyDTO request)
        {
            var response = await _walletService.AddMoney(request);
            if (response.Data != null) return Ok(response);
            return BadRequest(response);
        }
        [HttpPost(ApiRoutes.Wallet.SendMoney)]
        public async Task<IActionResult> SendMoney(SendMoneyDTO request)
        {
            var response = await _walletService.SendMoney(request);
            if (response.Data != null) return Ok(response);
            return BadRequest(response);
        }
        // Return paginated data
        [HttpGet(ApiRoutes.Wallet.GetWalletTransactions)]
        public async Task<IActionResult> GetWalletsTrans(int skip=0, int take=10)
        {
            var response = await _walletService.GetWalletTransactions(skip, take);
            if (response.Data != null) return Ok(response);
            return NotFound(response);
        }

        [HttpGet(ApiRoutes.Wallet.GetWalletBalances)]
        public async Task<IActionResult> GetWalletBalances(bool walletbalance, bool pointbalance)
        {
            var response = await _walletService.GetWalletBalances(walletbalance, pointbalance);
            if (response.Data != null) return Ok(response);
            return NotFound(response);
        }

        [HttpPost(ApiRoutes.Wallet.SpendMoney)]
        public async Task<IActionResult> SpendMoney(SpendMoneyDTO reqModel)
        {
            var response = await _walletService.SpendMoney(reqModel);
            if (response.Data != null) return Ok(response);
            return BadRequest(response);
        }
    }
}
