using System.Collections.Generic;
using Bluebeam.Data.Data;

namespace Bluebeam.Database.Repositories
{
    public interface IUserRepo
    {
        void AddUser(User user);
        User FindById(int id);
        IEnumerable<User> GetAll();

    }
}
