namespace FriendFinder.Data.Requests
{
    public sealed class UserFriendRequest : Request
    {
        public int UserId { get; set; }
        public int FriendId { get; set; }
    }
}
