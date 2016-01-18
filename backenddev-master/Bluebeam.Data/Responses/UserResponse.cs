using System;
using Bluebeam.Data.Data;

namespace Bluebeam.Data.Responses
{
    public class UserResponse
    {
        public UserResponse(User user)
        {
            if (null == user)
                throw new ArgumentNullException();

            UserId = user.Id;
            UserName = user.Name;
        }

        public int UserId { get; set; }
        public string UserName { get; set; }

        public override string ToString()
        {
            return string.Format("{0}. {1}", UserId, UserName);
        }
    }
}