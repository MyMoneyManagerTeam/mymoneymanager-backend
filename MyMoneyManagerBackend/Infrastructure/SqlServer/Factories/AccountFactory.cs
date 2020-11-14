using System.Data.SqlClient;
using Domain.Accounts;
using Infrastructure.SqlServer.Accounts;

namespace Infrastructure.SqlServer.Factories
{
    public class AccountFactory: IInstanceFromReaderFactory<IAccount>
    {
        public IAccount CreateFromReader(SqlDataReader reader)
        {
            return new Account()
            {
                Id = reader.GetGuid(reader.GetOrdinal(AccountSqlServer.ColumnId)),
                Balance = reader.GetDouble(reader.GetOrdinal(AccountSqlServer.ColumnBalance))
            };
        }
    }
}