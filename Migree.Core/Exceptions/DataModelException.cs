using System;

namespace Migree.Core.Exceptions
{
    public class DataModelException : Exception
    {
        public DataModelException(string message)
            : base(message)
        { }
    }
}
