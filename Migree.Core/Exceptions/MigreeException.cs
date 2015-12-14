using System;
using System.Net;

namespace Migree.Core.Exceptions
{
    public abstract class MigreeException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public MigreeException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
