using System.Collections.Generic;
using System.Web.Http;
using Bluebeam.Data.Responses;
using Bluebeam.ServiceBase;
using frdsCntrl = Bluebeam.Controllers.ServiceControllers.FriendsController;

namespace Bluebeam.Controllers
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
            return BluebeamServiceBase.GetResponse(userId, (a, result) => _friendsController.GetAllPotentialFriends(userId, result));
        }
    }
}
