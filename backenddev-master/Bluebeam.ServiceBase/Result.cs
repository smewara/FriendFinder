namespace Bluebeam.ServiceBase
{
    public class Result
    {
        public string Status { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Status, Message);
        }
    }
}
