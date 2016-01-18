using System;
using System.Collections.Generic;
using System.Linq;
using FriendFinder.Data.Data;
using FriendFinder.Data.Responses;

namespace FriendFinder.Database.Repositories
{
    public sealed class FriendRepo : IFriendRepo
    {
        private readonly IUserRepo _userRepo;
      
        public FriendRepo(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
      
        public bool AddFriend(int userId, int friendId)
        {
            return Add(userId, friendId);
        }

        private bool Add(int userId, int friendId)
        {
            //Check userId is not same as friendId. Avoid adding oneself as a friend.
            if (userId == friendId)
                throw new Exception("Cannot add yourself as a friend");

            lock (_userRepo)
            {
                //Get User
                User user;
                try
                {
                    user = _userRepo.FindById(userId);
                }
                catch (KeyNotFoundException)
                {
                    throw new KeyNotFoundException("Invalid user");
                }

                //Check if friend is a valid user.
                User frd;
                try
                {
                    frd = _userRepo.FindById(friendId);
                }
                catch (KeyNotFoundException)
                {
                    throw new KeyNotFoundException("Friend doesn't exist");
                }

                if (!user.Friends.Contains(frd))
                {
                    user.Friends.Add(frd);
                }

                if (!frd.Friends.Contains(user))
                {
                    frd.Friends.Add(user);
                }
            }

            return true;
        }


        public bool RemoveFriend(int userId, int friendId)
        {
            //Check userId is not same as friendId. Avoid adding oneself as a friend.
            if (userId == friendId)
                throw new Exception("Cannot remove yourself as a friend");

            lock (_userRepo)
            {
                User user, frd;

                //Get User
                try
                {
                    user = _userRepo.FindById(userId);
                }
                catch (KeyNotFoundException)
                {
                    throw new KeyNotFoundException("Invalid user");
                }

                //Check if friend is a valid user.
                try
                {
                    frd = _userRepo.FindById(friendId);
                }
                catch (KeyNotFoundException)
                {
                    throw new KeyNotFoundException("Friend doesn't exist");
                }


                if (user.Friends.Contains(frd))
                {
                    user.Friends.Remove(frd);
                }
                else
                {
                    throw new Exception(string.Format("User {0} is not a friend of {1}", frd.Id, user.Id));
                }

                if (frd.Friends.Contains(user))
                {
                    frd.Friends.Remove(user);
                }
                else
                {
                    throw new Exception(string.Format("User {0} is not a friend of {1}", user.Id, frd.Id));
                }
            }

            return true;
        }

        public IList<UserResponse> GetFriends(int userId)
        {
            try
            {
                var user = _userRepo.FindById(userId);

                return user.Friends.Select(frd => new UserResponse(frd)).ToList();
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException("Invalid user");
            }
        }

     
    }
}
