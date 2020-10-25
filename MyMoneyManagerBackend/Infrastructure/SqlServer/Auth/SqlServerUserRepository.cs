using System.Collections.Generic;
using System.Data;
using Domain.Users;

namespace Infrastructure.SqlServer.Auth
{
    public class SqlServerUserRepository : IUserRepository
    {
        public static readonly string TableName = "users";
        public static readonly string ColumnId = "id";
        public static readonly string ColumnMail = "mail";
        public static readonly string ColumnPassword = "password";
        public static readonly string ColumnAccount = "account";
        public static readonly string ColumnFirstName = "firstname";
        public static readonly string ColumnLastName = "lastname";
        
        public static readonly string ReqGet = $@"SELECT * FROM {TableName} WHERE {ColumnMail}=@{ColumnMail} AND {ColumnPassword}=@{ColumnPassword}";
        
        private IUserFactory _userFactory = new UserFactory(); 
        
        public IEnumerable<IUser> Query()
        {
            throw new System.NotImplementedException();
        }

        public IUser Get(string mail, string password)
        {
            IUser user = null;
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = ReqGet;
                command.Parameters.AddWithValue($"@{ColumnMail}", mail);
                command.Parameters.AddWithValue($"@{ColumnPassword}", password);

                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    user = _userFactory.CreateFromReader(reader);
                }
            }

            return user;
        }

        public IUser Create(IUser user)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(int id, IUser user)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}