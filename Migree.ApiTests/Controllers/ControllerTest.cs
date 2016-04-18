using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Migree.ApiTests.Setup;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Web.Http;

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

        protected JsonObject GetResultFromRequest<JsonObject, Controller>(Controller controller, Func<Controller, HttpResponseMessage> action)
            where Controller : MigreeApiController
        {
            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());
            var result = action(controller);
            var json = result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<JsonObject>(json);
        }

        [TestCleanup]
        public void TearDown()
        {
            Scope.Dispose();
        }
    }
}
