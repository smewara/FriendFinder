using System;

namespace Bluebeam.Data.Data
{
    public class UniqueId
    {
        private static readonly Lazy<UniqueId> LazyInstance = new Lazy<UniqueId>(() => new UniqueId());
        public static UniqueId Instance
        {
            get
            {
                return LazyInstance.Value;
            }
        }

        private readonly object _nextIdLock = new object();
        private int _nextId;

        public int NextId()
        {
            lock (_nextIdLock)
            {
                return ++_nextId;
            }
        }

        public static int Next()
        {
            return Instance.NextId();
        }
    }
}