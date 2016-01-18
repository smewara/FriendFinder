using Bluebeam.Data.Requests;
using Bluebeam.Data.Responses;
using System.Collections.Generic;
using System.Web.Http;
using Bluebeam.ServiceBase;
using userCntrl = Bluebeam.Controllers.ServiceControllers.UsersController;

namespace Bluebeam.Controllers
{
    /// <summary>
    /// User Controll
    /// </summary>
    public class UsersController : ApiController, IBluebeamServiceBase
    {
        private readonly userCntrl _usersController;
        /// <summary>
        /// Initialize the controller
        /// </summary>
        /// <param name="usersController"></param>
        public UsersController(userCntrl usersController)
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
           return BluebeamServiceBase.GetResponse<UserRequest>(request, (a, result) => _usersController.AddUser(request.Parameter, result));
        }


        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("users")]
        public Response<List<UserResponse>> Get()
        {
            return BluebeamServiceBase.GetResponse(result => _usersController.Get(result));
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
            return BluebeamServiceBase.GetResponse(result => _usersController.Get(userId, result));
        }



    }
}