using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WalletPlusApi.Core.Common.Responses;
using WalletPlusApi.Core.Data;
using WalletPlusApi.Core.DTOs;
using WalletPlusApi.Core.Util;
using WalletPlusApi.Core.Util.Email;
using WalletPlusApi.Core.Util.Email.Models;
using WalletPlusApi.Infrastructure.Persistence;
using WalletPlusApi.Infrastructure.Services.Interfaces;

namespace WalletPlusApi.Infrastructure.Services.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepo;
        private readonly IWalletService _walletService;
        private readonly IEmailSender _mailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CustomerService> _log;
        private readonly IConfiguration configuration;

        public CustomerService(IRepository<Customer> customerRepo, IEmailSender mailSender, IWalletService walletService, IHttpContextAccessor httpContextAccessor, ILogger<CustomerService> log, IConfiguration configuration)
        {
            _customerRepo = customerRepo;
            _mailSender = mailSender;
            _walletService = walletService;
            _httpContextAccessor = httpContextAccessor;
            _log = log;
            this.configuration = configuration;
        }

        /// <summary>
        /// Service method to log a user in.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseResponse> Login(LoginDTO model)
        {
            if (model == null) return null;
            Customer cst = new Customer();
            var userContext = _httpContextAccessor.HttpContext.User.Identity.Name;
            //_httpContextAccessor.HttpContext.Request.
            //get bsae url wt above
            _log.LogInformation($"Attempting to retrieve user {userContext} info.");
           

            cst = await _customerRepo.Get(u => u.Email == model.Email);
            if (cst == null)
            {
                _log.LogError($"User {model.Email} doesnt exist!");

                return Response.BadRequest(null);
            }

            var verifyPwd = AuthUtil.VerifyPasswordHash(model.Password, cst.PasswordHash, cst.PasswordSalt);
            if (!verifyPwd) return Response.BadRequest(null);

            var claims = new ClaimsIdentity(new[] { new Claim("id", $"{cst.Id}"), new Claim(ClaimTypes.Email, cst.Email), new Claim(ClaimTypes.Name, cst.Email) });
            var jwtSecret = configuration["JwtSettings:Secret"];
            var token = AuthUtil.GenerateJwtToken(jwtSecret, claims);
            claims.AddClaim(new Claim("token", token));

            var refreshToken = AuthUtil.GenerateRefreshToken();

            // Save tokens to DB
            cst.AuthToken = token;
            cst.RefreshToken = refreshToken;

             _customerRepo.Update(cst);
            var resp = new LoginResponseDTO
            {
                AuthToken = token,
                RefreshToken = refreshToken,
                Email = cst.Email,
            };
            _log.LogInformation($"user {cst.Email} login successful.");
            return Response.Ok(resp);
            //throw new NotImplementedException("h");
        }

        /// <summary>
        /// Service method to sign up a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseResponse> SignUp(CustomerDTO model)
        {
            var userExists = await _customerRepo.Get(r => r.Email == model.Email);
            if (userExists != null)
            {
                // return (customer: null, message: $"User {userExists.FirstName} exists.");
                return Response.BadRequest(null, $"User {userExists.FirstName} exists.");
            }
            // Validate Email-add in-app
            var (IsValid, Email) = AuthUtil.ValidateEmail(model.Email);

            if (model.Password.Length == 0)
            {
                return Response.BadRequest(null, $"Check password length.");
            }

            if (userExists == null)
            {
                AuthUtil.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
                var customer = new Customer
                {
                    CreatedAt = DateTime.Now,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    SecretKey=UtilMethods.GenerateSecretkey(),
                    LastUpdated=DateTime.Now,
                    PhoneNumber = model.PhoneNumber,
                    IsActive = true,
                    IsVerified=true 
                };

                //create both wallets
                //Send verification notification 
                string digits = UtilMethods.GenerateRandomString(6);
                var prepMessageDetails = new EmailMessage();
                prepMessageDetails.ToEmail = model.Email;
                prepMessageDetails.Subject = $"Verify your Email";
                prepMessageDetails.Body = $"Your OTP is {digits}.";

                // TODO: Set expiry on OTP.
                customer.OTP = digits;

                await _customerRepo.Add(customer);
                var (id, IsSaved) = await _customerRepo.Save(); 

                var (createdWallet, message) = await _walletService.CreateWallet(model);
                //send email to user
               // var sendmail = await _mailSender.SendEmailAsync(prepMessageDetails);

                if (createdWallet != null)
                {
                    var createCst = new CreateCustomerResponseDTO().PopulateDTO(model, createdWallet);
                    createCst.EncryptedSecKey = customer.SecretKey;
                    return Response.Ok(createCst, $"Customer {model.FirstName} created successfully.");
                }
            }
            return Response.BadRequest(null, $"Something went wrong.");
        }
    }
}
