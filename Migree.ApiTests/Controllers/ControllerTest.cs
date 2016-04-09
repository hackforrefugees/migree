using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Migree.ApiTests.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migree.ApiTests.Controllers
{
    public abstract class ControllerTest
    {
        private static IContainer container = null;
        private static IContainer Container
        {
            get
            {
                if (container == null)
                    container = AutofacConfig.RegisterDependencies();

                return container;
            }
        }

        private ILifetimeScope Scope;

        [TestInitialize]
        public void Setup()
        {
            Scope = Container.BeginLifetimeScope();
        }

        [TestCleanup]
        public void TearDown()
        {
            Scope.Dispose();
        }
    }
}
