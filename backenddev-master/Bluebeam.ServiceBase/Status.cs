using System.Runtime.Serialization;

namespace Bluebeam.ServiceBase
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