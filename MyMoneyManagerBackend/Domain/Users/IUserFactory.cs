using System.Data.SqlClient;

namespace Domain.Users
{
    public interface IUserFactory
    {
        IUser CreateFromParam(string mail, string password, string firstName, string lastName, byte[] picture, string country, string area, string address, int zip, string city);
        IUser GetFromParam(string mail, string password);
    }
}