using System.Data.SqlClient;
using Domain.Accounts;

namespace Infrastructure.SqlServer.Accounts
{
    public interface IAccountFactory
    {
        IAccount CreateFromReader(SqlDataReader reader);
    }
}