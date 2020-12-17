using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Application.Repositories;
using Domain.Users;
using Infrastructure.SqlServer.Accounts;
using Infrastructure.SqlServer.Factories;
using Microsoft.AspNetCore.Http;
using UserFactory = Infrastructure.SqlServer.Factories.UserFactory;

namespace Infrastructure.SqlServer.Auth
{
    public class UserRepository : IUserRepository
    {
        
        private IInstanceFromReaderFactory<IUser> _userFactory = new UserFactory();
        
        public IEnumerable<IUser> Query()
        {
            List<IUser> userList = new List<IUser>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = UserSqlServer.ReqQuery;
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    userList.Add(_userFactory.CreateFromReaderWithAccount(reader));
                }
            }

            return userList;
        }

        public IUser Get(string mail, string password)
        {
            IUser user = null;
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = UserSqlServer.ReqGet;
                command.Parameters.AddWithValue($"@{UserSqlServer.ColumnMail}", mail);
                command.Parameters.AddWithValue($"@{UserSqlServer.ColumnPassword}", password);

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
                command.CommandText = UserSqlServer.ReqCreate;
                command.Parameters.AddWithValue($"@{UserSqlServer.ColumnMail}", user.Mail.ToLower());
                command.Parameters.AddWithValue($"@{UserSqlServer.ColumnPassword}", user.Password);
                command.Parameters.AddWithValue($"@{UserSqlServer.ColumnFirstName}", user.FirstName);
                command.Parameters.AddWithValue($"@{UserSqlServer.ColumnLastName}", user.LastName);
                command.Parameters.AddWithValue($"@{UserSqlServer.ColumnCountry}", user.Country);
                command.Parameters.AddWithValue($"@{UserSqlServer.ColumnArea}", user.Area);
                command.Parameters.AddWithValue($"@{UserSqlServer.ColumnAddress}", user.Address);
                command.Parameters.AddWithValue($"@{UserSqlServer.ColumnZipCode}", user.Zip);
                command.Parameters.AddWithValue($"@{UserSqlServer.ColumnCity}", user.City);
                //command.Parameters.AddWithValue($"@{ColumnPicture}", ((object)user.Picture) ?? DBNull.Value);
                try
                {
                    user.Id = (Guid) command.ExecuteScalar();
                    // Ici on décide de créer un compte bancaire si l'utilisateur s'est inscrit
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
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

        public bool UploadImage(Guid userId, IFormFile image)
        {
            using (var ms = new MemoryStream())
            {
                image.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string s = Convert.ToBase64String(fileBytes);
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = UserSqlServer.ReqUpload;
                    cmd.Parameters.AddWithValue($"@{UserSqlServer.ColumnId}",userId);
                    cmd.Parameters.AddWithValue($"@{UserSqlServer.ColumnPicture}", fileBytes);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}