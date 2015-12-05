using System;
using System.Net;

namespace Migree.Core
{
    public static class HttpStatusCodeExtension
    {
        public static bool IsSuccess(this HttpStatusCode statusCode)
        {
            var statusAsInteger = Convert.ToInt32(statusCode);
            return statusAsInteger >= 200 && statusAsInteger < 300;
        }
    }
}
