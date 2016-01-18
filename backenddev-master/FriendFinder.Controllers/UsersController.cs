using System;
using System.Collections.Generic;
using System.Linq;
using FriendFinder.Data.Data;
using FriendFinder.Data.Requests;
using FriendFinder.Data.Responses;
using FriendFinder.Database.Repositories;
using FriendFinder.ServiceBase;

namespace FriendFinder.ServiceControllers
{
    public class UsersController
    {
        private readonly IUnitOfWork _unitOfWork;
        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddUser(UserRequest request, Result result)
        {
            try
            {
                _unitOfWork.UserRepo.AddUser(request.ToUser(UniqueId.Next()));


                result.Status = Status.Success.ToString();
                result.Message = string.Format("User {0} successfully created", request.UserName);


            }
            catch (Exception ex)
            {
                result.Status = Status.Error.ToString();
                result.Message = string.Format("Error in adding user {0}. {1}", request.UserName, ex.Message);
            }


        }

        public List<UserResponse> Get(Result result)
        {
            try
            {
                var query = from u in _unitOfWork.UserRepo.GetAll()
                            select new UserResponse(u);

                result.Status = Status.Success.ToString();

                return query.ToList();
            }
            catch(Exception ex)
            {
                result.Status = Status.Error.ToString();
                result.Message = string.Format("Error in getting all users. {0}", ex.Message);
            }

            return null;
        }

        public UserResponse Get(int userId, Result result)
        {
            try
            {
                var user = _unitOfWork.UserRepo.FindById(userId);

                result.Status = Status.Success.ToString();

                return new UserResponse(user);
            }
            catch(Exception ex)
            {
                result.Status = Status.Error.ToString();
                result.Message = string.Format("Error in getting user {0}. {1}", userId, ex.Message);
            }

            return null;
        }
    }
}
