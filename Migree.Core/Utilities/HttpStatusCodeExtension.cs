using System;
using System.Net;

namespace Migree
{
    public static class HttpStatusCodeExtension
    {
        public static bool IsSuccess(this HttpStatusCode statusCode)
        {
            var statusAsInteger = Convert.ToInt32(statusCode);
            return statusAsInteger.IsSuccess();            
        }

        public static bool IsSuccess(this int statusCode)
        {
            return statusCode >= 200 && statusCode < 300;
        }
    }
}
