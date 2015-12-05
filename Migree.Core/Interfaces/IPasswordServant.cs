namespace Migree.Core.Interfaces
{
    public interface IPasswordServant
    {        
        string CreateHash(string password);
        bool ValidatePassword(string password, string correctHash);
    }
}
