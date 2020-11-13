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
                Stream stream = reader.GetStream(reader.GetOrdinal(UserRepository.ColumnPicture));
                BinaryReader br = new BinaryReader(stream);
                img = br.ReadBytes((int) stream.Length);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return new User()
            {
                Id = reader.GetGuid(reader.GetOrdinal(UserRepository.ColumnId)),
                Mail = reader.GetString(reader.GetOrdinal(UserRepository.ColumnMail)),
                FirstName = reader.GetString(reader.GetOrdinal(UserRepository.ColumnFirstName)),
                Password = reader.GetString(reader.GetOrdinal(UserRepository.ColumnPassword)),
                LastName = reader.GetString(reader.GetOrdinal(UserRepository.ColumnLastName)),
                Address = reader.GetString(reader.GetOrdinal(UserRepository.ColumnAddress)),
                Admin = reader.GetBoolean(reader.GetOrdinal(UserRepository.ColumnAdmin)),
                Picture = img,
                Token = "fake-token", // FAKE TOKEN POUR L'INSTANT AVANT D'EN GEN UN VRAI
                Area = reader.GetString(reader.GetOrdinal(UserRepository.ColumnArea)),
                City = reader.GetString(reader.GetOrdinal(UserRepository.ColumnCity)),
                Confirmed = reader.GetBoolean(reader.GetOrdinal(UserRepository.ColumnConfirmed)),
                Country = reader.GetString(reader.GetOrdinal(UserRepository.ColumnCountry)),
                Zip = reader.GetInt32(reader.GetOrdinal(UserRepository.ColumnZipCode))
            };
        }
    }
}