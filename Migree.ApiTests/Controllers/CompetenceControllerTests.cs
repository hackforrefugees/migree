using Microsoft.VisualStudio.TestTools.UnitTesting;
using Migree.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using System.Text;
using System.Threading.Tasks;
using Migree.Core.Interfaces;

namespace Migree.Api.Controllers.Tests
{
    [TestClass()]
    public class CompetenceControllerTests : ControllerTest
    {        
        [TestMethod()]
        public void GetCompetencesTest()
        {
            var controller = new CompetenceController(Scope.Resolve<ICompetenceServant>(), Scope.Resolve<IBusinessServant>());
            //var items = controller.GetCompetences();
            Assert.Fail();
        }

        [TestMethod()]
        public void AddCompetenceTest()
        {
            Assert.Fail();
        }
    }
}