using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Application.Exceptions;
using Application.Repositories;
using Application.Services.Transactions;
using Application.Services.Transactions.Dto;
using Domain.Accounts;
using Domain.Jars;
using Domain.Transactions;
using Domain.Users;
using Infrastructure.SqlServer.Accounts;
using Infrastructure.SqlServer.Jars;
using Infrastructure.SqlServer.Transactions;
using NSubstitute;
using NUnit.Framework;

namespace UnitTest
{
    public class TransactionServicesTest
    {
        [Test]
        public void Query_userIdAndNumberAndPageAndDays_ReturnIEnumerableOutputDtoQueryTransaction()
        {
            //Arrange
            Guid myGuid = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}");
            Guid transactionId1 = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8340}");
            Guid transactionId2 = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8341}");
            ITransactionRepository transactionRepository = Substitute.For<ITransactionRepository>();
            IJarRepository jarRepository = Substitute.For<IJarRepository>();
            IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
            TransactionService transactionService = new TransactionService(transactionRepository,jarRepository,accountRepository);
            IUser user = new User
            {
                Id = myGuid
            };
            IEnumerable<Transaction> transactionsList = new List<Transaction>
            {
                new Transaction
                {
                    Id = transactionId1,
                    Amount = 100,
                    Description = "test",
                    Emitter = user,
                    Receiver = user,
                    TransactionDate = new DateTime(),
                    EmitterNameCustom = "test",
                    ReceiverNameCustom = "test"
                },
                new Transaction
                {
                    Id = transactionId2,
                    Amount = 100,
                    Description = "test",
                    Emitter = user,
                    Receiver = user,
                    TransactionDate = new DateTime(),
                    EmitterNameCustom = "test",
                    ReceiverNameCustom = "test"
                }
            };
            
            transactionRepository.Query(myGuid, 0, 0, 0).Returns(transactionsList);

            //Act

            var outputTransactionsList = transactionService.Query(myGuid, 0, 0, 0);

            //Assert
            Assert.AreEqual(transactionsList.Count(),outputTransactionsList.Count());
        }

        [Test]
        public void Create_UseridAndInputDtoCreateTransaction_ReturnNegativeException()
        {
            //Arrange
            Guid myGuid = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}");
            Guid emitterGuid = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8340}");
            Guid receiverGuid = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8341}"); 
            ITransactionRepository transactionRepository = Substitute.For<ITransactionRepository>();
            IJarRepository jarRepository = Substitute.For<IJarRepository>();
            IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
            TransactionService transactionService = new TransactionService(transactionRepository,jarRepository,accountRepository);
            InputDtoCreateTransaction inputDtoCreateTransaction = new InputDtoCreateTransaction
            {
                Amount = -150,
                Description = "test",
                EmitterId = emitterGuid,
                EmitterName = "test",
                ReceiverId = receiverGuid,
                ReceiverName = "test"
            };
            
            //Act
            var ex = Assert.Throws<NegativeTransactionException>(() => transactionService.Create(myGuid, inputDtoCreateTransaction));
            
            //Assert
            Assert.That(ex.Message, Is.EqualTo("Transaction négative impossible"));

        }

        [Test]
        public void Create_UseridAndInputDtoCreateTransaction_ReturnNotEnoughMoneyException()
        {
            //Arrange
            Guid myGuid = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}");
            Guid emitterGuid = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8340}");
            Guid receiverGuid = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8341}"); 
            ITransactionRepository transactionRepository = Substitute.For<ITransactionRepository>();
            IJarRepository jarRepository = Substitute.For<IJarRepository>();
            IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
            TransactionService transactionService = new TransactionService(transactionRepository,jarRepository,accountRepository);
            InputDtoCreateTransaction inputDtoCreateTransaction = new InputDtoCreateTransaction
            {
                Amount = 150,
                Description = "test",
                EmitterId = emitterGuid,
                EmitterName = "test",
                ReceiverId = receiverGuid,
                ReceiverName = "test"
            };
            IAccount emitterAccount = new Account
            {
                Id = emitterGuid,
                Balance = 200
            };
            accountRepository.Get(emitterGuid).Returns(emitterAccount);
            jarRepository.TotalBalanceByUserId(emitterGuid).Returns(100.0);
           
            //Act
            var ex = Assert.Throws<NotEnoughMoneyException>(() => transactionService.Create(myGuid, inputDtoCreateTransaction));
            
            //Assert
            Assert.That(ex.Message, Is.EqualTo("Votre solde est insuffisant"));
        }
        
    }
}