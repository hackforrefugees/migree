using System;

namespace Migree.Core.Exceptions
{
    public abstract class MigreeException : Exception
    {
        public MigreeException(string message)
            : base(message)
        { }
    }
}
