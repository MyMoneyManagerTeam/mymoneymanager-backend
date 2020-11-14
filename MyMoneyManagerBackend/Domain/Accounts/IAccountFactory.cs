using System;
using System.Data.SqlClient;
using Domain.Accounts;
using Domain.Transactions;

namespace Domain.Accounts
{
    public interface IAccountFactory
    {
        public IAccount GetFromParam(Guid id, double balance);
    }
}