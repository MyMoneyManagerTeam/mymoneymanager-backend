using System.Data.SqlClient;
using Domain.Transactions;

namespace Infrastructure.SqlServer.Transactions
{
    public interface ITransactionFactory
    {
        ITransaction CreateFromReader(SqlDataReader reader);
    }
}