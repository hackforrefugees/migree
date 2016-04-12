using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Migree.ApiTests.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Migree.Api.Controllers.Tests
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

        protected ILifetimeScope Scope { get; private set; }

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
