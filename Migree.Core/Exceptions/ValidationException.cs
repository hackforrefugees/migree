namespace Migree.Core.Exceptions
{
    public class ValidationException : MigreeException
    {
        public ValidationException(string message)
            : base(message)
        { }
    }
}
