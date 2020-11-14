using System;
using System.Data.SqlClient;
using System.IO;
using Domain.Users;
using Infrastructure.SqlServer.Auth;

namespace Infrastructure.SqlServer.Factories
{
    public class UserFactory: IInstanceFromReaderFactory<IUser>
    {
        public IUser CreateFromReader(SqlDataReader reader)
        {
            byte[] img = null;
            try
            {
                Stream stream = reader.GetStream(reader.GetOrdinal(UserSqlServer.ColumnPicture));
                BinaryReader br = new BinaryReader(stream);
                img = br.ReadBytes((int) stream.Length);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return new User()
            {
                Id = reader.GetGuid(reader.GetOrdinal(UserSqlServer.ColumnId)),
                Mail = reader.GetString(reader.GetOrdinal(UserSqlServer.ColumnMail)),
                FirstName = reader.GetString(reader.GetOrdinal(UserSqlServer.ColumnFirstName)),
                Password = reader.GetString(reader.GetOrdinal(UserSqlServer.ColumnPassword)),
                LastName = reader.GetString(reader.GetOrdinal(UserSqlServer.ColumnLastName)),
                Address = reader.GetString(reader.GetOrdinal(UserSqlServer.ColumnAddress)),
                Admin = reader.GetBoolean(reader.GetOrdinal(UserSqlServer.ColumnAdmin)),
                Picture = img,
                Area = reader.GetString(reader.GetOrdinal(UserSqlServer.ColumnArea)),
                City = reader.GetString(reader.GetOrdinal(UserSqlServer.ColumnCity)),
                Confirmed = reader.GetBoolean(reader.GetOrdinal(UserSqlServer.ColumnConfirmed)),
                Country = reader.GetString(reader.GetOrdinal(UserSqlServer.ColumnCountry)),
                Zip = reader.GetInt32(reader.GetOrdinal(UserSqlServer.ColumnZipCode))
            };
        }
    }
}