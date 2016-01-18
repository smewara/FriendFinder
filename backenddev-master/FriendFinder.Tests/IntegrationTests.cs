using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FriendFinder.Data.Data;
using FriendFinder.Database.Repositories;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace FriendFinder.Tests
{
  
    public class UniqueIdMock
    {
        int _startIndex;
        readonly object _lock;

        public UniqueIdMock(int startIndex)
        {
            _startIndex = startIndex;
            _lock = new object();
        }

        public int Next()
        {
            lock(_lock)
            {
                return ++_startIndex;
            }
        }
    }

    public class MockUser 
    {
        public User CreateUser(string username, string pass, UniqueIdMock uniqueIdMock){
            return new User(uniqueIdMock.Next(), username, pass);
        }
    }


    [TestFixture]

    public class IntegrationTests
    {
        IUnitOfWork _unitOfWork;
        IDictionary<int, User> _usersDictionary;
        MockUser _mockUser;
        const int Total = 100;
        Random _random;
        private UniqueIdMock _uniqueIdMock;

        [SetUp]
        public void SetUp()
        {
            
            var container = new UnityContainer();

            container.RegisterType<IDictionary<int, User>, Dictionary<int, User>>(new ContainerControlledLifetimeManager(), new InjectionConstructor());

            _usersDictionary = container.Resolve<Dictionary<int, User>>();

            container.RegisterType<IUserRepo, UserRepo>(new ContainerControlledLifetimeManager());
            container.RegisterType<IFriendRepo, FriendRepo>(new ContainerControlledLifetimeManager());
            container.RegisterType<IFriendNetworkRepo, FriendNetworkRepo>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new ContainerControlledLifetimeManager(), new InjectionConstructor(_usersDictionary));
            container.RegisterType<ServiceControllers.FriendsController, ServiceControllers.FriendsController>();
            container.RegisterType<ServiceControllers.UsersController, ServiceControllers.UsersController>();

            _unitOfWork = container.Resolve<IUnitOfWork>();

            _mockUser = new MockUser();

            _random = new Random();

            _uniqueIdMock = new UniqueIdMock(0);

            for (var i = 1; i <= Total; i++)
            {
                add_new_user_test_dictionary("s" + i, i.ToString(CultureInfo.InvariantCulture));
            }
        }

      
        
        public void add_new_user_test_dictionary(string name, string password)
        {
            var user = _mockUser.CreateUser(name, password, _uniqueIdMock);

            _unitOfWork.UserRepo.AddUser(user);

            Assert.IsTrue(_usersDictionary.ContainsKey(user.Id));

            Assert.NotNull(_unitOfWork.UserRepo.FindById(user.Id));

            Assert.IsTrue(_unitOfWork.UserRepo.FindById(user.Id).Id == user.Id);

            Assert.IsTrue(_unitOfWork.UserRepo.FindById(user.Id).Name == user.Name);
        }

        [Test]
        public void Find_User_By_ID_Test()
        {
            var userId = _random.Next(1, Total);

            Assert.IsNotNull(_unitOfWork.UserRepo.FindById(userId));
        }

        [Test]
        public void Add_Friends_Test()
        {
            var user1 = _random.Next(1, Total);
            var user2 = _random.Next(1, Total);

            AddFriends(user1, user2);
        }

        private void AddFriends(int user1, int user2)
        {
            _unitOfWork.FriendRepo.AddFriend(user1, user2);

            var user1Frds = _unitOfWork.UserRepo.FindById(user1).Friends;

            var user2Frds = _unitOfWork.UserRepo.FindById(user2).Friends;

            Assert.IsNotNull(user1Frds);

            Assert.IsNotNull(user2Frds);

            Assert.IsTrue(user1Frds.Select(each => each.Id).Contains(user2));

            Assert.IsTrue(user2Frds.Select(each => each.Id).Contains(user1));
        }

        [Test]
        public void Remove_Friend_Test()
        {
            var user1 = _random.Next(1, Total);
            var user2 = _random.Next(1, Total);

            AddFriends(user1, user2);

            _unitOfWork.FriendRepo.RemoveFriend(user1, user2);

            var user1Frds = _unitOfWork.UserRepo.FindById(user1).Friends;

            var user2Frds = _unitOfWork.UserRepo.FindById(user2).Friends;

            Assert.IsFalse(user1Frds.Select(each => each.Id).Contains(user2));

            Assert.IsFalse(user2Frds.Select(each => each.Id).Contains(user1));
        }

        [Test]
        public void Get_All_Potential_Friends_Test()
        {
          for(var i = 1; i <= _usersDictionary.Keys.Count -1; i++)
          {
              AddFriends(i, i + 1);
          }
          
          var notIncludedList = new [] {1, 2};

          var potentialFriends = _unitOfWork.FriendNetworkRepo.GetPotentialFriends(1);

          Assert.IsNotEmpty(potentialFriends);

          Assert.IsTrue(potentialFriends.Count() == _usersDictionary.Keys.Count - 2);

          var containsForbidden = potentialFriends.Any(each => notIncludedList.Contains(each.UserId));

          Assert.IsTrue(!containsForbidden);

          _unitOfWork.FriendRepo.RemoveFriend(1, 2);

          potentialFriends = _unitOfWork.FriendNetworkRepo.GetPotentialFriends(1);

          Assert.IsEmpty(potentialFriends);

        }

        [Test]
        public void Get_User_All_Friends_Test()
        {
            var frds = new List<int>();

            for(var i = 0; i < 5; i++)
            {
                frds.Add(_random.Next(2, Total));
            }

            foreach(var frdId in frds)
            {
                AddFriends(1, frdId);
            }

            var allfrds = _unitOfWork.FriendRepo.GetFriends(1);

            Assert.IsNotEmpty(allfrds);

            Assert.IsTrue(allfrds.Count() == 5);

            Assert.IsTrue(allfrds.All(each => frds.Contains(each.UserId)));

            foreach(var frdId in frds)
            {
                _unitOfWork.FriendRepo.RemoveFriend(frdId, 1);
            }

            allfrds = _unitOfWork.FriendRepo.GetFriends(1);

            Assert.IsEmpty(allfrds);


        }

    }
}
