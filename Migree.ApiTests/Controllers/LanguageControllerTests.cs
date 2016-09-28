﻿using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Migree.Core.Interfaces;
using Migree.Core.Models.Language;

namespace Migree.Api.Controllers.Tests
{
    [TestClass()]
    public class LanguageControllerTests : ControllerTest
    {
        [TestMethod()]
        public void GetTest()
        {
            var controller = GetLanguageController();
            var result = GetResultFromRequest<Client, LanguageController>(controller, (ctrl) => { return ctrl.Get("en"); });

            Assert.AreEqual("Login", result.Home["loginButton"]);
            Assert.AreEqual("Start conversation", result.Message["startThread"]);
        }

        private LanguageController GetLanguageController()
        {
            return new LanguageController(Scope.Resolve<ILanguageServant>()); 
        }
    }
}