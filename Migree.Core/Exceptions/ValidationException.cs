using System.Net;

namespace Migree.Core.Exceptions
{
    public class ValidationException : MigreeException
    {
        public ValidationException(HttpStatusCode statusCode, string message)
            : base(statusCode, message)
        { }
    }
}
