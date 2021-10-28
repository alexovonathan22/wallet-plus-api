using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IEmailSender _mailSender;

        public CustomerService(IRepository<Customer> customerRepo)
        {
            _customerRepo = customerRepo;
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
                return new BaseResponse
                {
                    Data = null,
                    Message=$"User {userExists.FirstName} exists.",
                    Status="fail",
                    StatusCode="01"
                };
            }
            // Validate Email-add in-app
            var (IsValid, Email) = AuthUtil.ValidateEmail(model.Email);

            if (model.Password.Length == 0)
            {
                //return (customer: null, message: $"Email format incorrect. or Password not inputed");
                return new BaseResponse
                {
                    Data = null,
                    Message = $"Check password length.",
                    Status = "fail",
                    StatusCode = "01"
                };
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
                    LastUpdated=DateTime.Now,
                    PhoneNumber = model.PhoneNumber
                };

                //create both wallets

                //Send verification notification 
                Random rand = new Random();
                string digits = rand.Next(0, 999999).ToString("D6");
                var prepMessageDetails = new EmailMessage();
                prepMessageDetails.ToEmail = model.Email;
                prepMessageDetails.Subject = $"Verify your Email";
                prepMessageDetails.Body = $"Your OTP is {digits}.";

                // TODO: Set expiry on OTP.
                customer.OTP = digits;

                await _customerRepo.Add(customer);

                //send email to user
                var sendmail = await _mailSender.SendEmailAsync(prepMessageDetails);

                if (sendmail)
                {

                    return new BaseResponse
                    {
                        Data = model,
                        Message = $"Customer {userExists.FirstName} created successfully.",
                        Status = "success",
                        StatusCode = "00"
                    };
                }
            }
            return new BaseResponse
            {
                Data = model,
                Message = $"Customer {userExists.FirstName} created successfully.",
                Status = "success",
                StatusCode = "00"
            };
            ;
        }
    }
}
