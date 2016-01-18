using System;
using System.Collections.Generic;
using System.Linq;
using FriendFinder.Data.Requests;
using FriendFinder.Data.Responses;
using FriendFinder.Database.Repositories;
using FriendFinder.ServiceBase;

namespace FriendFinder.ServiceControllers
{
    public class FriendsController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FriendsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddFriend(UserFriendRequest request, Result result)
        {
            try
            {
                _unitOfWork.FriendRepo.AddFriend(request.UserId, request.FriendId);

                result.Status = Status.Success.ToString();
                result.Message = string.Format("Successfully added {0} and {1} as friends", request.FriendId, request.UserId);
            }
            catch (Exception ex)
            {
                result.Status = Status.Error.ToString();
                result.Message = string.Format("Error adding user {0} as friend of user {1}. Reason :: {2}", request.FriendId, request.UserId, ex.Message);
            }

        }

        public void DeleteFriend(UserFriendRequest request, Result result)
        {
            try
            {
                _unitOfWork.FriendRepo.RemoveFriend(request.UserId, request.FriendId);

                result.Status = Status.Success.ToString();
                result.Message = string.Format("Successfully removed {0} and {1} as friends", request.FriendId, request.UserId);
            }
            catch (Exception ex)
            {
                result.Status = Status.Error.ToString();
                result.Message = string.Format("Error deleting friends {0}. Reason :: {1}", request, ex.Message);
            }

        }

        public IEnumerable<UserResponse> Get(int userId, Result result)
        {
            try
            {
                var frds = _unitOfWork.FriendRepo.GetFriends(userId);

                result.Status = Status.Success.ToString();

                return frds;
            }
            catch (Exception ex)
            {
                result.Status = Status.Error.ToString();
                result.Message = string.Format("Error getting user {0}'s friends. {1}", userId, ex.Message);
            }

            return null;
        }

        public IEnumerable<UsersConnectionResponse> GetShortestConnection(UserFriendRequest request, Result result)
        {
            try
            {
                var allPossiblePaths = _unitOfWork.FriendNetworkRepo.GetAllPaths(request.UserId, request.FriendId).ToList();

                var allPaths = allPossiblePaths.Select(path => path as IList<UserResponse> ?? path.ToList()).Select(userResponses => new UsersConnectionResponse
                {
                    Count = userResponses.Count(), Path = string.Join("->", userResponses.Select(each => each.UserName))
                }).ToList();

                result.Status = Status.Success.ToString();

                return allPaths.OrderBy(each=>each.Count);
            }
            catch (Exception ex)
            {
                result.Status = Status.Error.ToString();
                result.Message = string.Format("Error in getting the connection between {0} and {1}. Reason :: {2}", request.UserId, request.FriendId, ex.Message);
            }

            return null;
        }

        public IEnumerable<UserResponse> GetAllPotentialFriends(int userId, Result result)
        {
            try
            {
                var potentialFriends = _unitOfWork.FriendNetworkRepo.GetPotentialFriends(userId);
                result.Status = Status.Success.ToString();

                return potentialFriends;
            }
            catch (Exception ex)
            {
                result.Status = Status.Error.ToString();
                result.Message = string.Format("Error in getting potential friends of user {0}. Reason :: {1}", userId, ex.Message);
            }

            return null;
        }
    }
}
