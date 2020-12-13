using System;
using System.Collections.Generic;
using System.Linq;
using Application.Exceptions;
using Application.Repositories;
using Application.Services.Transactions.Dto;
using Domain.Jars;
using Domain.Transactions;
using Domain.Users;

namespace Application.Services.Transactions
{
    public class TransactionService: ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IJarRepository _jarRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionFactory _transactionFactory = new TransactionFactory();

        public TransactionService(ITransactionRepository transactionRepository, IJarRepository jarRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _jarRepository = jarRepository;
            _accountRepository = accountRepository;
        }

        public IEnumerable<OutputDtoQueryTransaction> Query(Guid userId,int number, int page,int days) //automapper lib pour transformer les types dto<->model
        {
            return _transactionRepository
                .Query(userId,number,page, days)
                .Select(transaction => new OutputDtoQueryTransaction
                {
                    Amount = transaction.Amount,
                    Description = transaction.Description,
                    Id = transaction.Id,
                    EmitterId = transaction.Emitter.Id,
                    EmitterName = transaction.EmitterNameCustom,
                    ReceiverId = transaction.Receiver.Id,
                    ReceiverName = transaction.ReceiverNameCustom,
                    TransactionDate = transaction.TransactionDate
                });
        }

        public OutputDtoCreateTransaction Create(Guid userId, InputDtoCreateTransaction transaction) 
        {
            if (transaction.Amount>(_accountRepository.Get(userId).Balance-_jarRepository.TotalBalanceByUserId(userId)))
            {
                //serait cool de throw une erreur solde insuffisant (custom exception)
                throw new NotEnoughMoneyException("Votre solde est insuffisant");
            }
            var transactionFromDto = _transactionFactory.CreateFromParam(new User{ Id= transaction.EmitterId}, new User{Id = transaction.ReceiverId},
                transaction.Amount, DateTime.Now, transaction.Description, transaction.EmitterName,
                transaction.ReceiverName);
            var transactionInDb = _transactionRepository.Create(transactionFromDto);
            if (transactionInDb == null)
            {
                return null;
            }
            else //si l'ajout est correct on passe ici
            {
                _accountRepository.ModifyBalance(transactionInDb.Emitter.Id, -transactionInDb.Amount);
                _accountRepository.ModifyBalance(transactionInDb.Receiver.Id, transactionInDb.Amount);
            }
            return new OutputDtoCreateTransaction
            {
                Amount = transactionInDb.Amount,
                Description = transactionInDb.Description,
                Id = transactionInDb.Id,
                EmitterId = transactionInDb.Emitter.Id,
                EmitterName = transactionInDb.EmitterNameCustom,
                ReceiverId = transactionInDb.Receiver.Id,
                ReceiverName = transactionInDb.ReceiverNameCustom,
                TransactionDate = transactionInDb.TransactionDate
            }; 
        }

        public int CountTransactions(Guid guid)
        {
            return _transactionRepository.CountTransactions(guid);
        }
    }
}