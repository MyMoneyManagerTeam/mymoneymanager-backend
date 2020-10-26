using Domain.Shared;

namespace Domain.Users
{
    public interface IUser: IEntity
    {
        int Id { get; set; }
        string Mail { get; set; }
        string Password { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        byte[] Picture { get; set; }
        string Account { get; set; }
        string Token { get; set; }
    }
}