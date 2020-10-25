using System.Data.SqlClient;
using Domain.Users;

namespace Infrastructure.SqlServer.Auth
{
    public class UserFactory: IUserFactory
    {
        public IUser CreateFromReader(SqlDataReader reader)
        {
            return new User()
            {
                Id = reader.GetInt32(reader.GetOrdinal(SqlServerUserRepository.ColumnId)),
                Account = reader.GetString(reader.GetOrdinal(SqlServerUserRepository.ColumnAccount)),
                Mail = reader.GetString(reader.GetOrdinal(SqlServerUserRepository.ColumnMail)),
                FirstName = reader.GetString(reader.GetOrdinal(SqlServerUserRepository.ColumnFirstName)),
                Password = reader.GetString(reader.GetOrdinal(SqlServerUserRepository.ColumnPassword)),
                LastName = reader.GetString(reader.GetOrdinal(SqlServerUserRepository.ColumnLastName)),
                Token = "fake-token",
            };
        }
    }
}