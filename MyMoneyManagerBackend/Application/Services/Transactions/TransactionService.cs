using System;
using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Application.Services.Transactions.Dto;
using Domain.Jars;
using Domain.Transactions;

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

        public IEnumerable<OutputDtoQueryTransaction> Query(Guid userId)
        {
            return _transactionRepository
                .Query(userId)
                .Select(transaction => new OutputDtoQueryTransaction
                {
                    Amount = transaction.Amount,
                    Description = transaction.Description,
                    Id = transaction.Id,
                    EmitterId = transaction.EmitterId,
                    EmitterName = transaction.EmitterName,
                    ReceiverId = transaction.ReceiverId,
                    ReceiverName = transaction.ReceiverName,
                    TransactionDate = transaction.TransactionDate
                });
        }

        public OutputDtoCreateTransaction Create(Guid userId, InputDtoCreateTransaction transaction)
        {
            if (userId == transaction.EmitterId)
            {
                return null;
            }

            if (transaction.Amount>(_accountRepository.Get(userId).Balance-_jarRepository.TotalBalanceByUserId(userId)))
            {
                //serait cool de throw une erreur solde insuffisant
                return null;
            }
            var transactionFromDto = _transactionFactory.CreateFromParam(transaction.EmitterId, transaction.ReceiverId,
                transaction.Amount, DateTime.Now, transaction.Description, transaction.EmitterName,
                transaction.ReceiverName);
            var transactionInDb = _transactionRepository.Create(transactionFromDto);
            if (transactionInDb == null)
            {
                return null;
            }
            else //si l'ajout est correct on passe ici
            {
                _accountRepository.ModifyBalance(transactionInDb.EmitterId, -transactionInDb.Amount);
                _accountRepository.ModifyBalance(transactionInDb.ReceiverId, transactionInDb.Amount);
            }
            return new OutputDtoCreateTransaction
            {
                Amount = transactionInDb.Amount,
                Description = transactionInDb.Description,
                Id = transactionInDb.Id,
                EmitterId = transactionInDb.EmitterId,
                EmitterName = transactionInDb.EmitterName,
                ReceiverId = transactionInDb.ReceiverId,
                ReceiverName = transactionInDb.ReceiverName,
                TransactionDate = transactionInDb.TransactionDate
            };        }
    }
}