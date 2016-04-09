using Autofac;
using Migree.Core.Autofac;
using Migree.Core.Definitions;
using Migree.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migree.ApiTests.Setup
{
    public static class AutofacConfig
    {
        public static IContainer RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new CoreRegistrationModule(ApplicationType.Runtime));
            //builder.RegisterType<MockDataRepository>().As<IDataRepository>().InstancePerLifetimeScope();
            //builder.RegisterType<MockPushNotificationRepository>().As<IPushNotificationRepository>().InstancePerLifetimeScope();
            return builder.Build();
        }
    }
}
