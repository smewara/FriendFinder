using System.Collections.Generic;
using System.Web.Http;
using Bluebeam.Data.Requests;
using Bluebeam.Data.Responses;
using Bluebeam.ServiceBase;
using frdsCntrl = Bluebeam.Controllers.ServiceControllers.FriendsController;

namespace Bluebeam.Controllers
{
    /// <summary>
    /// Friends Controller
    /// </summary>
    public class FriendsController : ApiController, IBluebeamServiceBase
    {
        private readonly frdsCntrl _friendsController;
        
        /// <summary>
        /// Initializing friends controller
        /// </summary>
        /// <param name="friendsController"></param>
        public FriendsController(frdsCntrl friendsController)
        {
            _friendsController = friendsController;
        }

        /// <summary>
        /// Add a user as friend
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("friends")]
        public Response Put(Request<UserFriendRequest> request)
        {
            return BluebeamServiceBase.GetResponse<UserFriendRequest>(request, (req, result) => _friendsController.AddFriend(request.Parameter, result));  
        }

        /// <summary>
        /// Remove a friend
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="friendId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("friends")]
        public Response Delete(int userId, int friendId)
        {
            var request = new UserFriendRequest { UserId = userId, FriendId = friendId };

            return BluebeamServiceBase.GetResponse(request, (req, result) => _friendsController.DeleteFriend(request, result));
        }


        /// <summary>
        /// Get All friends of a specific user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("friends/{userId}")]
        public Response<IEnumerable<UserResponse>> Get(int userId)
        {

            return BluebeamServiceBase.GetResponse(userId, (a, result) => _friendsController.Get(userId, result));
        }

        /// <summary>
        /// Find the connection between two users
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("friends")]
        public Response<IEnumerable<UsersConnectionResponse>> Post(UserFriendRequest request)
        {
            return BluebeamServiceBase.GetResponse(request, (req, result) => _friendsController.GetShortestConnection(request, result));
        }

     
    }
}
