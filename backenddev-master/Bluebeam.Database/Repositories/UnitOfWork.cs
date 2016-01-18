using System;
using System.Collections.Generic;
using Bluebeam.Data.Data;

namespace Bluebeam.Database.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Lazy<IUserRepo> _userRepo;
        private readonly Lazy<IFriendRepo> _friendRepo;
        private readonly Lazy<IFriendNetworkRepo> _friendNetworkRepo;
        public IUserRepo UserRepo { get { return _userRepo.Value; } }
        public IFriendRepo FriendRepo { get { return _friendRepo.Value; } }
        public IFriendNetworkRepo FriendNetworkRepo { get { return _friendNetworkRepo.Value; } }

        public UnitOfWork(IDictionary<int, User> userDictionary)
        {
            _userRepo = new Lazy<IUserRepo>(() => new UserRepo(userDictionary));
            _friendRepo = new Lazy<IFriendRepo>(() => new FriendRepo(_userRepo.Value));
            _friendNetworkRepo = new Lazy<IFriendNetworkRepo>(() => new FriendNetworkRepo(_userRepo.Value));
        }

        
    }
}