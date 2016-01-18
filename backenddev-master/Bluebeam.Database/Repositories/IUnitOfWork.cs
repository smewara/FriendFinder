namespace Bluebeam.Database.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepo UserRepo { get; }
        IFriendRepo FriendRepo { get; }
        IFriendNetworkRepo FriendNetworkRepo { get; }
    }
}