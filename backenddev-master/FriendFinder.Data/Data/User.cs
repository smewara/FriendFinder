using System.Collections.Generic;
using System.Security;
using FriendFinder.Data.Utilities;

namespace FriendFinder.Data.Data
{
    public class User
    {
        public User(int id, string name, string password)
            : this(id, name, password.ToSecureString())
        {
            Friends = new List<User>();
        }

        public User(int id, string name, SecureString password)
        {
            _id = id;
            _name = name;
            _password = password;
            FavoriteShowIds = new List<int>();
            Friends = new List<User>();
        }

        private readonly int _id;
        private readonly string _name;
        private readonly SecureString _password;

        public int Id { get { return _id; } }
        public string Name { get { return _name; } }
        internal SecureString Password { get { return _password; } }
        public List<int> FavoriteShowIds { get; private set; }
        public List<User> Friends { get; private set; }
    }
}