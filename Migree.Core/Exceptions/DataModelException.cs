namespace Migree.Core.Exceptions
{
    public class DataModelException : MigreeException
    {
        public DataModelException()
            : base(System.Net.HttpStatusCode.InternalServerError, "An error occured, data couldn´t be handled")
        { }
    }
}
