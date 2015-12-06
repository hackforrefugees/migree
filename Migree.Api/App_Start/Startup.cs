using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Migree.Core.Autofac;
using Migree.Core.Definitions;
using Migree.Api.Providers;
using Owin;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: OwinStartup(typeof(Migree.Api.Startup))]
namespace Migree.Api
{
    public class Startup
    {
        private const int TOKEN_EXPIRE_DAYS = 30;

        public void Configuration(IAppBuilder app)
        {
            ConfigureCors(app);

            var config = new HttpConfiguration();

            ConfigureAutofac(app, config);
            ConfigureOAuth(app);
            ConfigureRoutesAndFilters();

            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }

        private void ConfigureCors(IAppBuilder app)
        {
            app.UseCors(new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context => Task.FromResult(new CorsPolicy
                    {
                        AllowAnyHeader = true,
                        AllowAnyMethod = true,
                        AllowAnyOrigin = true,
                        SupportsCredentials = false,
                        PreflightMaxAge = Int32.MaxValue
                    })
                }
            }); ;
        }

        private void ConfigureRoutesAndFilters()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);            
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(TOKEN_EXPIRE_DAYS),
                Provider = new AuthorizationServerProvider()
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
        public void ConfigureAutofac(IAppBuilder app, HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            builder.RegisterModule(new CoreRegistrationModule(ApplicationType.Web));
            builder.RegisterControllers(executingAssembly);
            builder.RegisterApiControllers(executingAssembly);

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            app.UseAutofacMiddleware(container);
        }
    }
}
