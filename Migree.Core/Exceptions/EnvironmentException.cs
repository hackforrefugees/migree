using System;

namespace Migree.Core.Exceptions
{
    public class EnvironmentException : MigreeException
    {
        public EnvironmentException(string message)
            : base(System.Net.HttpStatusCode.InternalServerError, message)
        { }
    }
}
