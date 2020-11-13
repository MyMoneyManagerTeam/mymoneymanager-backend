using System;
using System.Data.SqlClient;
using System.IO;
using Domain.Users;

namespace Domain.Users
{
    public class UserFactory: IUserFactory
    {
        public IUser CreateFromParam(string mail, string password, string firstName, string lastName, byte[] picture, string country,
            string area, string address, int zip, string city)
        {
            return new User
            {
                Address = address,
                Mail = mail,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Picture = picture,
                Country = country,
                Area = area,
                Zip = zip,
                City = city
            };
        }

        public IUser GetFromParam(string mail, string password)
        {
            return new User
            {
                Mail = mail,
                Password = password
            };
        }
    }
}