using Autofac;
using Autofac.Integration.WebApi;
using Migree.ApiTests.Mocks;
using Migree.Core.Autofac;
using Migree.Core.Definitions;
using Migree.Core.Interfaces;
using System.Reflection;

namespace Migree.ApiTests.Setup
{
    public static class AutofacConfig
    {
        public static IContainer RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new CoreRegistrationModule(ApplicationType.Runtime));
            builder.RegisterType<MockSettingsServant>().As<ISettingsServant>().SingleInstance();
            builder.RegisterType<MockDataRepository>().As<IDataRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MockContentRepository>().As<IContentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MockMailRepository>().As<IMailRepository>().InstancePerLifetimeScope();
            builder.RegisterApiControllers(Assembly.GetAssembly(typeof(CoreRegistrationModule)));
            return builder.Build();
        }
    }
}
