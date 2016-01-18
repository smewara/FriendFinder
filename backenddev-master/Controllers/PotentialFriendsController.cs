using System.Collections.Generic;
using System.Web.Http;
using FriendFinder.Data.Responses;
using FriendFinder.ServiceBase;
using frdsCntrl = FriendFinder.ServiceControllers.FriendsController;

namespace FriendFinder.Controllers
{
    /// <summary>
    /// Potential friends controller
    /// </summary>
    public class PotentialFriendsController : ApiController
    {
        private readonly frdsCntrl _friendsController;
        
        /// <summary>
        /// Initializing friends controller
        /// </summary>
        /// <param name="friendsController"></param>
        public PotentialFriendsController(frdsCntrl friendsController)
        {
            _friendsController = friendsController;
        }

        /// <summary>
        /// Get All potential friends of a specific user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("potentialfriends/{userId}")]
        public Response<IEnumerable<UserResponse>> Get(int userId)
        {
            return FriendFinderServiceBase.GetResponse(userId, (a, result) => _friendsController.GetAllPotentialFriends(userId, result));
        }
    }
}
