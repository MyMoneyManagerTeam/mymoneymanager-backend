using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Net;
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
        public static readonly string ColumnCity = "city";
        public static readonly string ColumnAddress = "address";
        public static readonly string ColumnZipCode = "zipcode";
        public static readonly string ColumnArea = "area";
        public static readonly string ColumnCountry = "country";
        public static readonly string ColumnPicture = "picture";
        
        public static readonly string ReqGet = $@"SELECT * FROM {TableName} WHERE {ColumnMail}=@{ColumnMail} AND {ColumnPassword}=@{ColumnPassword}";
        public static readonly string ReqCreate = $@"INSERT INTO {TableName}
            ({ColumnMail},{ColumnPassword},{ColumnFirstName},{ColumnLastName},{ColumnPicture},{ColumnCountry},{ColumnArea},{ColumnAddress},{ColumnZipCode},{ColumnCity})
            OUTPUT INSERTED.{ColumnId}            
            VALUES (@{ColumnMail},@{ColumnPassword},@{ColumnFirstName},@{ColumnLastName},NULL,@{ColumnCountry},@{ColumnArea},@{ColumnAddress},@{ColumnZipCode},@{ColumnCity});
        ";
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
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                // (@{ColumnMail},@{ColumnPassword},@{ColumnFirstName},@{ColumnLastName},@{ColumnPicture},@{ColumnCountry},@{ColumnArea},@{ColumnAddress},@{ColumnZipCode},@{ColumnCity})
                command.CommandText = ReqCreate;
                command.Parameters.AddWithValue($"@{ColumnMail}", user.Mail);
                command.Parameters.AddWithValue($"@{ColumnPassword}", user.Password);
                command.Parameters.AddWithValue($"@{ColumnFirstName}", user.FirstName);
                command.Parameters.AddWithValue($"@{ColumnLastName}", user.LastName);
                command.Parameters.AddWithValue($"@{ColumnCountry}", user.Country);
                command.Parameters.AddWithValue($"@{ColumnArea}", user.Area);
                command.Parameters.AddWithValue($"@{ColumnAddress}", user.Address);
                command.Parameters.AddWithValue($"@{ColumnZipCode}", user.ZipCode);
                command.Parameters.AddWithValue($"@{ColumnCity}", user.City);
                try
                {
                    user.Id = (int) command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                }
                
            }
            return user;
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