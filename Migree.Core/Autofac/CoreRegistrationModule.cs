using Autofac;
using Migree.Core.Definitions;
using Migree.Core.Interfaces;
using Migree.Core.Repositories;
using Migree.Core.Servants;

namespace Migree.Core.Autofac
{
    public class CoreRegistrationModule : Module
    {
        private ApplicationType ApplicationType { get; }

        public CoreRegistrationModule(ApplicationType applicationType)
        {
            ApplicationType = applicationType;
        }

        protected override void Load(ContainerBuilder builder)
        {
            Register<SettingsServant, ISettingsServant>(builder);
            Register<AzureTableRepository, IDataRepository>(builder);
            Register<AzureBlobRepository, IContentRepository>(builder);
            base.Load(builder);
        }

        private void Register<Concrete, Interface>(ContainerBuilder builder)
        {
            if (ApplicationType == ApplicationType.Web)
            {
                builder.RegisterType<Concrete>().As<Interface>().InstancePerRequest();
            }
            else //runtime
            {
                builder.RegisterType<Concrete>().As<Interface>().InstancePerLifetimeScope();
            }
        }
    }
}
