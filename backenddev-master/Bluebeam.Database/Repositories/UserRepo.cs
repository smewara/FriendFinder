using System;
using System.Collections.Generic;
using System.Linq;
using Bluebeam.Data.Data;

namespace Bluebeam.Database.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly IDictionary<int, User> _users;
      
        public UserRepo(IDictionary<int, User> users)
        {
            _users = users;
        }
      
        public void AddUser(User user)
        {
            if (null == user)
                throw new ArgumentNullException();

            _users.Add(user.Id, user);
        }

        public User FindById(int id)
        {
            if (_users.ContainsKey(id))
                return _users[id];

            throw new KeyNotFoundException();
        }

        public IEnumerable<User> GetAll()
        {
            return _users.Values.ToList();
        }

    }
}