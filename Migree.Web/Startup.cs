﻿using Autofac;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Migree.Core.Autofac;
using Migree.Core.Definitions;
using Migree.Web.Providers;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.Owin.Cors;
using System.Threading.Tasks;
using System.Web.Cors;

[assembly: OwinStartup(typeof(Migree.Web.Startup))]
namespace Migree.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureCors(app);
            
            HttpConfiguration config = new HttpConfiguration();

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
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(30),
                Provider = new AuthorizationServerProvider()
            };

            // Token Generation
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
