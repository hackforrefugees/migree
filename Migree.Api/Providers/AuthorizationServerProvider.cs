using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Migree.Core.Exceptions;
using Migree.Core.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Migree.Api.Providers
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private IUserServant UserServant { get; }

        public AuthorizationServerProvider()
        {
            UserServant = DependencyResolver.Current.GetService<IUserServant>();
        }
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                var user = UserServant.FindUser(context.UserName, context.Password);
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("userName", context.UserName));
                identity.AddClaim(new Claim("userId", user.Id.ToString()));
                var clientProperties = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "userId", user.Id.ToString()
                    }
                });
                var ticket = new AuthenticationTicket(identity, clientProperties);
                context.Validated(ticket);
            }
            catch (ValidationException e)
            {
                context.SetError("invalid_grant", e.Message);
                return;
            }
            catch
            {
                context.SetError("invalid_grant", "General error");
                return;
            }
        }
    }
}
