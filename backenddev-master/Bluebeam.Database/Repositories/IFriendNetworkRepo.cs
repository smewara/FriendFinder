using System.Collections.Generic;
using Bluebeam.Data.Responses;

namespace Bluebeam.Database.Repositories
{
    public interface IFriendNetworkRepo
    {
        IEnumerable<IEnumerable<UserResponse>> GetAllPaths(int userIdA, int userIdB);
        IList<UserResponse> GetPotentialFriends(int userId);
    }
}