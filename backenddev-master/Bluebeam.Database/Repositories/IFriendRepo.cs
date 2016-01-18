using System.Collections.Generic;
using Bluebeam.Data.Responses;

namespace Bluebeam.Database.Repositories
{
    public interface IFriendRepo
    {
        bool AddFriend(int userId, int friendId);
        bool RemoveFriend(int userId, int friendId);
        IList<UserResponse> GetFriends(int id);
    }
}