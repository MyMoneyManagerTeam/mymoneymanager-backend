using System;
using System.Data.SqlClient;
using System.IO;
using Domain.Users;

namespace Infrastructure.SqlServer.Auth
{
    public class UserFactory: IUserFactory
    {
        public IUser CreateFromReader(SqlDataReader reader)
        {
            byte[] img = null;
            try
            {
                Stream stream = reader.GetStream(reader.GetOrdinal(SqlServerUserRepository.ColumnPicture));
                BinaryReader br = new BinaryReader(stream);
                img = br.ReadBytes((int) stream.Length);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return new User()
            {
                Id = reader.GetInt32(reader.GetOrdinal(SqlServerUserRepository.ColumnId)),
                Account = reader.GetString(reader.GetOrdinal(SqlServerUserRepository.ColumnAccount)),
                Mail = reader.GetString(reader.GetOrdinal(SqlServerUserRepository.ColumnMail)),
                FirstName = reader.GetString(reader.GetOrdinal(SqlServerUserRepository.ColumnFirstName)),
                Password = reader.GetString(reader.GetOrdinal(SqlServerUserRepository.ColumnPassword)),
                LastName = reader.GetString(reader.GetOrdinal(SqlServerUserRepository.ColumnLastName)),
                Picture = img,
                Token = "fake-token",
            };
        }
    }
}