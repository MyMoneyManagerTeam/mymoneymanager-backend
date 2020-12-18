using System;
using System.Text.Json.Serialization;
using Domain.Accounts;

namespace Domain.Users
{
    public class User: IUser
    {
        public User()
        {
        }

        public Guid Id { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public int Zip { get; set; }
        public string City { get; set; }
        public byte[] Picture { get; set; }
        public bool Confirmed { get; set; }
        public bool Admin { get; set; }
        
        public string Token { get; set; }
        
        public Account Account { get; set; }

        protected bool Equals(User other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            return Equals((User) obj);
        }
    }
}