using System;

namespace Migree.Core.Exceptions
{
    public class EnvironmentException : Exception
    {
        public EnvironmentException(string message)
            : base(message)
        { }
    }
}
