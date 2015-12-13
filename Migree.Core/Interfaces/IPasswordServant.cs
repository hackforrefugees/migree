namespace Migree.Core.Interfaces
{
    public interface IPasswordServant
    {        
        string CreatePasswordHash(string password);
        bool ValidatePassword(string password, string correctHash);
    }
}
