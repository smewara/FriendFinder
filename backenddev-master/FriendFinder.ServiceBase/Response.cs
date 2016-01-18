namespace FriendFinder.ServiceBase
{
    public class Response
    {
        public Result Result { get; set; }
    }

    public class Response<T> : Response
    {
        public T Data { get; set; }
    }
}