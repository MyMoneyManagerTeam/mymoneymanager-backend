using System;
using Application.Services.Accounts;
using Application.Services.Accounts.Dto;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class AccountRepositoryTest
    {
        private IAccountService _accountService;
        
        [SetUp]
        public void Init()
        { 
            _accountService = Substitute.For<IAccountService>();
        }

        [Test]
        public void Get_SingleId_OutputDtoGetAccount()
        {
            //Arrange
            OutputDtoGetAccount outputDtoGetAccount = new OutputDtoGetAccount
            {
                Id = new Guid("fd639119-ce4f-401f-959b-fb8999dc8344"),
                Balance = 100,
                AvailableBalance = 100
            };

            var id = outputDtoGetAccount.Id;
            var balance = outputDtoGetAccount.Balance;
            var availableBalance = outputDtoGetAccount.AvailableBalance;

            //Act
            _accountService.Get(new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}")).Returns(outputDtoGetAccount);
            
            //Assert
            Assert.That(_accountService.Get(new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}")),Is.EqualTo(outputDtoGetAccount));

        }
        
        [Test]
        public void Create_SingleId_OutputDtoCreateAccount()
        {
            //Arrange
            OutputDtoCreateAccount outputDtoCreateAccount = new OutputDtoCreateAccount
            {
                Id = new Guid("fd639119-ce4f-401f-959b-fb8999dc8344"),
                Balance = 100
            };
            
            //Act
            _accountService.Create(new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}")).Returns(outputDtoCreateAccount);
            
            //Assert
            Assert.That(_accountService.Create(new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}")),Is.EqualTo(outputDtoCreateAccount));
        }

        [Test]
        public void Update_IdAndInputDtoUpdateAccount_ReturnTrue()
        {
            //Arrange
            InputDtoUpdateAccount inputDtoUpdateAccount = new InputDtoUpdateAccount
            {
                Id = new Guid("fd639119-ce4f-401f-959b-fb8999dc8344"),
                Balance = 100,
            };
            
            //Act
            _accountService.Update(new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}"),inputDtoUpdateAccount).Returns(true);
            
            //Assert
            Assert.That(_accountService.Update(new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}"), inputDtoUpdateAccount), Is.EqualTo(true));
        }

        [Test]
        public void ModifyBalance_IdAndAmount_ReturnTrue()
        {
            //Arrange
            InputDtoModifyBalanceAccount inputDtoModifyBalanceAccount = new InputDtoModifyBalanceAccount
            {
                UserId = new Guid("fd639119-ce4f-401f-959b-fb8999dc8344"),
                Amount = 100
            };

            //Act
            _accountService.ModifyBalance(new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}"), 100).Returns(true);
            
            //Assert
            Assert.That(_accountService.ModifyBalance(new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}"), 100),Is.EqualTo(true));
        }
    }
}