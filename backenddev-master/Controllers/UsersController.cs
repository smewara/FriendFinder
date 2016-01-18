using System.Collections.Generic;
using System.Web.Http;
using FriendFinder.Data.Requests;
using FriendFinder.Data.Responses;
using FriendFinder.ServiceBase;
using usersCntrl = FriendFinder.ServiceControllers.UsersController;

namespace FriendFinder.Controllers
{
    /// <summary>
    /// User Controll
    /// </summary>
    public class UsersController : ApiController, IFriendFinderServiceBase
    {
        private readonly usersCntrl _usersController;
        /// <summary>
        /// Initialize the controller
        /// </summary>
        /// <param name="usersController"></param>
        public UsersController(usersCntrl usersController)
        {
            _usersController = usersController;
        }

        /// <summary>
        /// Add a new user
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        [Route("users")]
        public Response Post(Request<UserRequest> request)
        {   
           return FriendFinderServiceBase.GetResponse<UserRequest>(request, (a, result) => _usersController.AddUser(request.Parameter, result));
        }


        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("users")]
        public Response<List<UserResponse>> Get()
        {
            return FriendFinderServiceBase.GetResponse(result => _usersController.Get(result));
        }

        /// <summary>
        /// Get a specific user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("users/{userId}")]
        public Response<UserResponse> Get(int userId)
        {
            return FriendFinderServiceBase.GetResponse(result => _usersController.Get(userId, result));
        }



    }
}