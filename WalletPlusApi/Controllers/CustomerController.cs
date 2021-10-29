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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
       
        // POST api/<UserController>
        [HttpPost(ApiRoutes.User.NewUser)]
        [AllowAnonymous]
        public async Task<IActionResult> NewUser(CustomerDTO request)
        {
            var response = await _customerService.SignUp(request);
            if (response.Data != null) return Ok(response);
            return BadRequest(response);
        }
        [HttpPost(ApiRoutes.User.Login)]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO request)
        {
            var response = await _customerService.Login(request);
            if (response.Data != null) return Ok(response);
            return BadRequest(response);
        }

    }
}
