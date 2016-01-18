using System.Collections.Generic;
using FriendFinder.Data.Data;

namespace FriendFinder.Database.Repositories
{
    public interface IUserRepo
    {
        void AddUser(User user);
        User FindById(int id);
        IEnumerable<User> GetAll();

    }
}
