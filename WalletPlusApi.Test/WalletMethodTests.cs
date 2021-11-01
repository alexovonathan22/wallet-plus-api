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
using WalletPlusApi.Core.Util;

namespace WalletPlusApi.Test
{
    public class WalletMethodTests
    {

        public WalletMethodTests()
        {

        }

        // Arrange
        [Theory]
        [InlineData(1000, 0)]
        [InlineData(5000, 50)]
        [InlineData(10001, 250.025)]
        [InlineData(25001, 1250.05)]

        public void Test_PointEarned_When_Equal(decimal amt, decimal expected)
        {
            //Act
            var result = UtilMethods.CalculatePointsEarned(amt);

            //Assert
            Assert.Equal(expected, result);
        }

       
    }

   
}
