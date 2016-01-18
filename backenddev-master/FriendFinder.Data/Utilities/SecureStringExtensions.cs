using System;
using System.Runtime.InteropServices;
using System.Security;

namespace FriendFinder.Data.Utilities
{
    public static class SecureStringExtensions
    {
        public static string ToUnsecureString(this SecureString source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var secureString = IntPtr.Zero;

            try
            {
                secureString = Marshal.SecureStringToGlobalAllocUnicode(source);
                return Marshal.PtrToStringUni(secureString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(secureString);
            }
        }

        public static SecureString ToSecureString(this string source)
        {
            if (null == source)
                throw new ArgumentNullException("source");

            var result = new SecureString();

            foreach (var c in source)
                result.AppendChar(c);

            result.MakeReadOnly();

            return result;
        }
    }
}