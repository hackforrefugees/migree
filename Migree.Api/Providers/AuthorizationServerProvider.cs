using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Migree.Api.Configuration;
using Migree.Core.Exceptions;
using Migree.Core.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Migree.Api.Providers
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private const string INVALID_GRANT = "invalid_grant";
        private const string GENERAL_ERROR_MESSAGE = "General error message";

        private IUserServant UserServant { get; }

        public AuthorizationServerProvider()
        {
            UserServant = DependencyResolver.Current.GetService<IUserServant>();
        }

        /// <summary>
        ///    Called to validate that the origin of the request is a registered "client_id",
        ///     and that the correct credentials for that client are present on the request.
        ///     If the web application accepts Basic authentication credentials, context.TryGetBasicCredentials(out
        ///     clientId, out clientSecret) may be called to acquire those values if present
        ///     in the request header. If the web application accepts "client_id" and "client_secret"
        ///     as form encoded POST parameters, context.TryGetFormCredentials(out clientId,
        ///     out clientSecret) may be called to acquire those values if present in the request
        ///     body. If context.Validated is not called the request will not proceed further.
        /// </summary>
        /// <param name="context">The context of the event carries information in and results out.</param>
        /// <returns>Task to enable asynchronous execution</returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            await Task.Delay(1);
        }

        /// <summary>
        /// Called when a request to the Token endpoint arrives with a "grant_type" of "password".
        /// This occurs when the user has provided name and password credentials directly
        /// into the client application's user interface, and the client application is using
        /// those to acquire an "access_token" and optional "refresh_token". If the web application
        /// supports the resource owner credentials grant type it must validate the context.Username
        /// and context.Password as appropriate. To issue an access token the context.Validated
        /// must be called with a new ticket containing the claims about the resource owner
        /// which should be associated with the access token. The application should take
        /// appropriate measures to ensure that the endpoint isn’t abused by malicious callers.             
        /// The default behavior is to reject this grant type. See also http://tools.ietf.org/html/rfc6749#section-4.3.2
        /// </summary>
        /// <param name="context">The context of the event carries information in and results out.</param>
        /// <returns>Task to enable asynchronous execution</returns>        
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                var user = UserServant.FindUser(context.UserName, context.Password);
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);                
                identity.AddClaim(new Claim(Global.ClaimUserId, user.Id.ToString()));
                var ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
                context.Validated(ticket);
                await Task.Delay(1);
            }
            catch (ValidationException e)
            {
                context.SetError(INVALID_GRANT, e.Message);
                return;
            }
            catch
            {
                context.SetError(INVALID_GRANT, GENERAL_ERROR_MESSAGE);
                return;
            }
        }        
    }
}
