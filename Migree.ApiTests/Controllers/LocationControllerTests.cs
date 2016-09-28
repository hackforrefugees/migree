using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Migree.Api.Models;
using Migree.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Migree.Api.Controllers.Tests
{
    [TestClass()]
    public class LocationControllerTests : ControllerTest
    {
        [TestMethod()]
        public void GetAllTest()
        {
            var controller = GetLocationController();
            var result = GetResultFromRequest<IEnumerable<IntIdAndName>, LocationController>(controller, (ctrl) => { return ctrl.GetAll(); }).ToList();

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual("Other", result.Last().Name);
        }

        private LocationController GetLocationController()
        {
            return new LocationController(Scope.Resolve<ILanguageServant>());
        }
    }
}