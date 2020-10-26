using System.Text.Json.Serialization;

namespace Domain.Users
{
    public class User: IUser
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [JsonIgnore]
        public string Country { get; set; }
        [JsonIgnore]
        public string Area { get; set; }
        [JsonIgnore]
        public string Address { get; set; }
        [JsonIgnore]
        public int ZipCode { get; set; }
        [JsonIgnore]
        public string City { get; set; }
        public string Account { get; set; }
        public byte[] Picture { get; set; }
        public string Token { get; set; }

        public User()
        {
        }

        public User(int id, string mail, string password, string firstName, string lastName, string country, string area, string address, int zipCode, string city, string account, byte[] picture, string token)
        {
            Id = id;
            Mail = mail;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            Area = area;
            Address = address;
            ZipCode = zipCode;
            City = city;
            Account = account;
            Picture = picture;
            Token = token;
        }
    }
}