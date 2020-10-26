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
        public string Account { get; set; }
        public byte[] Picture { get; set; }
        public string Token { get; set; }

        public User()
        {
        }

        public User(int id, string mail, string password, string firstName, string lastName, string account, string token)
        {
            Id = id;
            Mail = mail;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Account = account;
            Token = token;
        }
    }
}