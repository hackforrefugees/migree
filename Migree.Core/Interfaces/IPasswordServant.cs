namespace Migree.Core.Interfaces
{
    public interface IPasswordServant
    {
        string CreateSalt();
        string CreateHash(string password, string salt);
        bool ValidatePassword(string password, string correctHash);
    }
}
