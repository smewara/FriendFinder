﻿using FriendFinder.Data.Data;

namespace FriendFinder.Data.Requests
{
    public sealed class UserRequest : Request
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public User ToUser(int id)
        {
            return new User(id, UserName, Password);
        }
    }
}