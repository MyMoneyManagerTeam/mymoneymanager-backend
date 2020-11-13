using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Application.Repositories;
using Domain.Users;
using Infrastructure.SqlServer.Factories;
using UserFactory = Infrastructure.SqlServer.Factories.UserFactory;

namespace Infrastructure.SqlServer.Auth
{
    public class UserRepository : IUserRepository
    {
        public static readonly string TableName = "users";
        public static readonly string ColumnId = "user_id";
        public static readonly string ColumnMail = "mail";
        public static readonly string ColumnPassword = "password";
        public static readonly string ColumnFirstName = "first_name";
        public static readonly string ColumnLastName = "last_name";
        public static readonly string ColumnCity = "city";
        public static readonly string ColumnAddress = "address";
        public static readonly string ColumnZipCode = "zip";
        public static readonly string ColumnArea = "area";
        public static readonly string ColumnCountry = "country";
        public static readonly string ColumnConfirmed = "confirmed";
        public static readonly string ColumnAdmin = "admin";
        public static readonly string ColumnPicture = "picture";
        
        public static readonly string ReqGet = $@"SELECT * FROM {TableName} WHERE {ColumnMail}=@{ColumnMail} AND {ColumnPassword}=@{ColumnPassword}";
        public static readonly string ReqCreate = $@"INSERT INTO {TableName}
            ({ColumnId},{ColumnMail},{ColumnPassword},{ColumnFirstName},{ColumnLastName},{ColumnCountry},{ColumnArea},{ColumnAddress},{ColumnZipCode},{ColumnCity},{ColumnPicture},{ColumnConfirmed},{ColumnAdmin})
            OUTPUT INSERTED.{ColumnId}            
            VALUES (NEWID(),@{ColumnMail},@{ColumnPassword},@{ColumnFirstName},@{ColumnLastName},@{ColumnCountry},@{ColumnArea},@{ColumnAddress},@{ColumnZipCode},@{ColumnCity},NULL,1,0);
        ";
        private IInstanceFromReaderFactory<IUser> _userFactory = new UserFactory();
        
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
                command.Parameters.AddWithValue($"@{ColumnMail}", user.Mail.ToLower());
                command.Parameters.AddWithValue($"@{ColumnPassword}", user.Password);
                command.Parameters.AddWithValue($"@{ColumnFirstName}", user.FirstName);
                command.Parameters.AddWithValue($"@{ColumnLastName}", user.LastName);
                command.Parameters.AddWithValue($"@{ColumnCountry}", user.Country);
                command.Parameters.AddWithValue($"@{ColumnArea}", user.Area);
                command.Parameters.AddWithValue($"@{ColumnAddress}", user.Address);
                command.Parameters.AddWithValue($"@{ColumnZipCode}", user.Zip);
                command.Parameters.AddWithValue($"@{ColumnCity}", user.City);
                //command.Parameters.AddWithValue($"@{ColumnPicture}", ((object)user.Picture) ?? DBNull.Value);
                try
                {
                    user.Id = (Guid) command.ExecuteScalar();
                }
                catch (SqlException ex)
                {
                    return null;
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