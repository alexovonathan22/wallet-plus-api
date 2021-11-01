using System;
using Xunit;
using Moq;
using WalletPlusApi.Infrastructure.Services.Interfaces;
using WalletPlusApi.Controllers;
using WalletPlusApi.Core.DTOs;
using WalletPlusApi.Core.Common.Responses;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WalletPlusApi.Test
{
    public class WalletControllerTests
    {
         private readonly Mock<IWalletService> _walletMock;
         private readonly WalletController _cut; // cut => Controller Under Tests
   
        public WalletControllerTests()
        {
            _walletMock = new Mock<IWalletService>();
            _cut = new WalletController(_walletMock.Object);
        }

        /* Unit Testing Controllers*/

        [Fact]
        public async Task Test_AddMoney_OkObj()
        {
            //Arrange
            var addmoneydto = new AddMoneyDTO
            {
                AmountToAdd = 1000,
                DepositingWalletId="0000044875",
                TopUpMode=1
            };
            var addMoneyRespdto = new AddMoneyResponseDTO
            {
                AmountToAdd = addmoneydto.AmountToAdd,
                DepositingWalletId = addmoneydto.DepositingWalletId,
                TopUpMode = "ThirdParty",
                Message = "success"
            };
            var resp = new BaseResponse
            {
                Data = addmoneydto,
                Message = addMoneyRespdto.Message,
                Status = "success",
                StatusCode = "00"
            };
            _walletMock.Setup(r => r.AddMoney(addmoneydto))
                .ReturnsAsync(resp);

            //Act
            var response = await _cut.AddMoney(addmoneydto);

            //Assert

            Assert.IsType<OkObjectResult>(response);
            
        }

        [Fact]
        public async Task Test_AddMoney_BadRequestObj()
        {
            //Arrange
            var addmoneydto = new AddMoneyDTO
            {
                AmountToAdd = 1000,
                DepositingWalletId = "0000044875",
                TopUpMode = 1
            };
            var addMoneyRespdto = new AddMoneyResponseDTO
            {
                AmountToAdd = addmoneydto.AmountToAdd,
                DepositingWalletId = addmoneydto.DepositingWalletId,
                TopUpMode = "ThirdParty",
                Message = "success"
            };
            var resp = new BaseResponse
            {
                Data = null,
                Message = addMoneyRespdto.Message,
                Status = "success",
                StatusCode = "00"
            };
            _walletMock.Setup(r => r.AddMoney(addmoneydto))
                .ReturnsAsync(resp);

            //Act
            var response = await _cut.AddMoney(addmoneydto);

            //Assert

            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task Test_GetWalletTransactions_NotFoundRequestObj()
        {
            //Arrange
            var resp = new BaseResponse
            {
                Data = null,
                Message = "Retrieved",
                Status = "success",
                StatusCode = "00"
            };
            _walletMock.Setup(r => r.GetWalletTransactions(0, 10))
                .ReturnsAsync(resp);

            //Act
            var response = await _cut.GetWalletsTrans();

            //Assert

            Assert.IsType<NotFoundObjectResult>(response);
        }

        [Fact]
        public async Task Test_GetWalletTransactions_OkRequestObj()
        {
            //Arrange
            var txnDTO = new List<TransactionResponseDTO>
                {
                    new TransactionResponseDTO
                    {

                    },
                    new TransactionResponseDTO
                    {

                    }
                };
            var resp = new BaseResponse
            {
                Data = txnDTO,
                Message = "Retrieved",
                Status = "success",
                StatusCode = "00"
            };
            _walletMock.Setup(r => r.GetWalletTransactions(0, 10))
                .ReturnsAsync(resp);

            //Act
            var response = await _cut.GetWalletsTrans();

            //Assert

            Assert.IsType<OkObjectResult>(response);
        }

        
    }
}
