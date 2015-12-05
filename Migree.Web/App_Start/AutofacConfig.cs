using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Migree.Core.Autofac;
using Migree.Core.Definitions;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace Migree.Web
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            Assembly executingAssembly = Assembly.GetExecutingAssembly();            
            builder.RegisterModule(new CoreRegistrationModule(ApplicationType.Web));

            builder.RegisterControllers(executingAssembly);
            builder.RegisterApiControllers(executingAssembly);
            
            var container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}