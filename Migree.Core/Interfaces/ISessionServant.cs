using System;

namespace Migree.Core.Interfaces
{
    public interface ISessionServant
    {
        Guid GetUserId(string authenticationKey);
        void SetAuthenticationKey(string authenticationKey, Guid userId);
    }
}
