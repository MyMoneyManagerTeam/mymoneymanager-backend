using System.Data.SqlClient;
using Domain.Users;

namespace Infrastructure.SqlServer.Auth
{
    public interface IUserFactory
    {
        IUser CreateFromReader(SqlDataReader reader);
    }
}