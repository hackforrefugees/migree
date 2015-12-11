using Migree.Core.Exceptions;
using System;
using System.Collections.Generic;

namespace Migree.Core.Servants
{
    public class SessionServant
    {
        private Dictionary<string, Guid> AuthenticationKeys { get; }

        public SessionServant()
        {
            AuthenticationKeys = new Dictionary<string, Guid>();
        }

        public Guid GetUserId(string authenticationKey)
        {
            if (!AuthenticationKeys.ContainsKey(authenticationKey))
            {
                throw new ValidationException("User hasn´t any logged in key");
            }

            return AuthenticationKeys[authenticationKey];
        }

        public void SetAuthenticationKey(string authenticationKey, Guid userId)
        {
            AuthenticationKeys.Add(authenticationKey, userId);
        }
    }
}
