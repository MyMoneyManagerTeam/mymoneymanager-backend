using System;
using System.Data.SqlClient;
using Domain.Transactions;

namespace Domain.Accounts
{
    public class AccountFactory: IAccountFactory
    {
        public IAccount GetFromParam(Guid id, double balance)
        {
            return new Account()
            {
                Balance = balance,
                Id = id
            };
        }
    }
}