using System;
using System.Collections.Generic;
using Application.Repositories;
using Application.Services.Accounts;
using Application.Services.Accounts.Dto;
using Domain.Accounts;
using Domain.Jars;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
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
            
        }

        [Test]
        public void Get_SingleId_OutputDtoGetAccount()
        {
            //Arrange
            IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
            IJarRepository jarRepository = Substitute.For<IJarRepository>();
            _accountService = new AccountService(accountRepository, jarRepository);
            
            OutputDtoGetAccount outputDtoGetAccount = new OutputDtoGetAccount
            {
                Id = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}"),
                Balance = 100,
                AvailableBalance = 150
            };
            
            IAccount iaccount = new Account
            {
                Balance = 100,
                Id = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}")
            };
            
            accountRepository.Get(new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}")).Returns(iaccount);

            IJar ijar = new Jar
            {
                Owner = null,
                Balance = 100,
                Description = null,
                Id = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}"),
                Max = 150,
                Name = null
            };
            
            jarRepository.TotalBalanceByUserId(new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}")).Returns(ijar.Balance);

            //Act
             OutputDtoGetAccount outputTest = _accountService.Get(new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}"));

            //Assert
            Assert.AreEqual(outputDtoGetAccount,outputTest);
        }
        
        [Test]
        public void Get_Guid_Null()
        {
            //Arrange
            IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
            IJarRepository jarRepository = Substitute.For<IJarRepository>();
            _accountService = new AccountService(accountRepository, jarRepository);

            accountRepository.Get(new Guid()).ReturnsNull();
            
            //Act
            OutputDtoGetAccount outputTest = _accountService.Get(new Guid());

            //Assert
            Assert.AreEqual(null,outputTest);
        }
        
        [Test]
        public void Create_SingleId_OutputDtoCreateAccount()
        {
            //Arrange
            IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
            IJarRepository jarRepository = Substitute.For<IJarRepository>();
            _accountService = new AccountService(accountRepository, jarRepository);
            
            OutputDtoCreateAccount outputDtoCreateAccount = new OutputDtoCreateAccount
            {
                Id = new Guid("fd639119-ce4f-401f-959b-fb8999dc8344"),
                Balance = 100
            };
            IAccount iaccount = new Account
            {
                Balance = 100,
                Id = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}")
            };
            accountRepository.Create(new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}")).Returns(iaccount);
            //Act
            var OutputTest = _accountService.Create(new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}"));
            
            //Assert
            Assert.AreEqual(outputDtoCreateAccount,OutputTest);
        }
        
        [Test]
        public void Create_SingleId_Null()
        {
            //Arrange
            IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
            IJarRepository jarRepository = Substitute.For<IJarRepository>();
            _accountService = new AccountService(accountRepository, jarRepository);
      
            accountRepository.Create(new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}")).ReturnsNull();
            
            //Act
            OutputDtoCreateAccount outputTest = _accountService.Create(new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}"));
            
            //Assert
            Assert.AreEqual(null,outputTest);
        }

        [Test]
        public void Update_IdAndInputDtoUpdateAccount_ReturnTrue()
        {
            //Arrange
            IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
            IJarRepository jarRepository = Substitute.For<IJarRepository>();
            _accountService = new AccountService(accountRepository, jarRepository);
            Guid myGuid = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}");
            
            InputDtoUpdateAccount inputDtoUpdateAccount = new InputDtoUpdateAccount
            {
                Id = myGuid,
                Balance = 100,
            };
            
            IAccount iaccount = new Account
            {
                Id = myGuid,
                Balance = 100
            };

            accountRepository.Update(myGuid, iaccount).Returns(true);
            
            //Act
            bool myBool = _accountService.Update(myGuid,inputDtoUpdateAccount);

            //Assert
            Assert.AreEqual(true,myBool);
        }

        [Test]
        public void ModifyBalance_IdAndAmount_ReturnTrue()
        {
            //Arrange
            IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
            IJarRepository jarRepository = Substitute.For<IJarRepository>();
            _accountService = new AccountService(accountRepository, jarRepository);
            Guid myGuid = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}");
            InputDtoModifyBalanceAccount inputDtoModifyBalanceAccount = new InputDtoModifyBalanceAccount
            {
                UserId = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}"),
                Amount = 100
            };
            accountRepository.ModifyBalance(myGuid, inputDtoModifyBalanceAccount.Amount).Returns(true);
            
            //Act
            var myBool = _accountService.ModifyBalance(myGuid, inputDtoModifyBalanceAccount.Amount);
            
            //Assert
            Assert.AreEqual(true,myBool);

        }
    }
}