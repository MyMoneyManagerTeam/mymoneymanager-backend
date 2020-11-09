using Domain.Shared;

namespace Domain.Accounts
{
    public interface IAccount: IEntity
    {
        double Balance { get; set; }
    }
}