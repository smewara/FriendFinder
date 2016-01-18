namespace FriendFinder.ServiceBase
{
    public class Request
    {
        public string Application { get; set; }
    }

    public class Request<T> : Request
    {
        public T Parameter { get; set; }
    }
}
