using System.Runtime.Serialization;

namespace FriendFinder.ServiceBase
{
    public enum Status
    {
        [EnumMember]
        Error = 0,

        [EnumMember]
        Warning = 1,

        [EnumMember]
        Success = 3,

    }
}