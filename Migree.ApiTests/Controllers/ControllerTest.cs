﻿using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Migree.Api.Providers;
using Migree.ApiTests.Mocks;
using Migree.ApiTests.Setup;
using Migree.Core.Interfaces;
using Migree.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        protected void SetUserAsLoggedIn(Core.Definitions.UserType userType)
        {
            var dataRepository = Scope.Resolve<IDataRepository>() as MockDataRepository;
            var user = dataRepository.GetMockModels<User>().First(x => x.UserType == userType);
            (Scope.Resolve<ISessionProvider>() as MockSessionProvider).CurrentUserId = user.Id;
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

        protected void SetRequest<Controller>(ref Controller controller)
            where Controller : MigreeApiController
        {
            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());
        }

        protected IEnumerable<Model> GetMockModels<Model>()
          where Model : StorageModel
        {
            return ((MockDataRepository)Scope.Resolve<IDataRepository>()).GetMockModels<Model>();
        }

        [TestCleanup]
        public void TearDown()
        {
            Scope.Dispose();
        }
    }
}