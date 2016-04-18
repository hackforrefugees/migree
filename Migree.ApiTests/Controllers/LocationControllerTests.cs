using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Migree.Api.Controllers;
using Migree.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migree.Api.Controllers.Tests
{
    [TestClass()]
    public class LocationControllerTests : ControllerTest
    {
        [TestMethod()]
        public void GetAllTest()
        {
            var controller = new LocationController(Scope.Resolve<ILanguageServant>());
            controller.GetAll();
            Assert.Fail();
        }
    }
}