using System;
using System.Collections.Generic;
using Application.Services.Transactions.Dto;

namespace Application.Services.Transactions
{
    public interface ITransactionService
    {
        IEnumerable<OutputDtoQueryTransaction> Query(Guid userId,int number,int page, int days);
        OutputDtoCreateTransaction Create(Guid userId,InputDtoCreateTransaction jar);
        int CountTransactions(Guid guid);
    }
}