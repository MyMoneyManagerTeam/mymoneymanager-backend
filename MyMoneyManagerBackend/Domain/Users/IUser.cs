using Domain.Accounts;
using Domain.Shared;

namespace Domain.Users
{
    public interface IUser: IEntity
    {
        string Mail { get; set; }
        string Password { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Country { get; set; }
        string Area { get; set; }
        string Address { get; set; }
        int Zip { get; set; }
        string City { get; set; } 
        byte[] Picture { get; set; }
        bool Confirmed { get; set; }
        bool Admin { get; set; }
        string Token { get; set; }
        Account Account { get; set; }
    }
}