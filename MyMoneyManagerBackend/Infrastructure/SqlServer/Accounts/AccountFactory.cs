using System.Data.SqlClient;
using Domain.Accounts;

namespace Infrastructure.SqlServer.Accounts
{
    public class AccountFactory: IAccountFactory
    {
        public IAccount CreateFromReader(SqlDataReader reader)
        {
            return new Account()
            {
                Id = reader.GetGuid(reader.GetOrdinal(SqlServerAccountRepository.ColumnId)),
                Balance = reader.GetDouble(reader.GetOrdinal(SqlServerAccountRepository.ColumnBalance))
            };
        }
    }
}