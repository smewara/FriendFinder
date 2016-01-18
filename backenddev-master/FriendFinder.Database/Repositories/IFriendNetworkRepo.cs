using System.Collections.Generic;
using FriendFinder.Data.Responses;

namespace FriendFinder.Database.Repositories
{
    public interface IFriendNetworkRepo
    {
        IEnumerable<IEnumerable<UserResponse>> GetAllPaths(int userIdA, int userIdB);
        IList<UserResponse> GetPotentialFriends(int userId);
    }
}