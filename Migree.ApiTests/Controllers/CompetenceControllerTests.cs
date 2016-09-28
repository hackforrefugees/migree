using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Migree.Api.Models.Responses;
using Migree.Core.Interfaces;
using System.Linq;

namespace Migree.Api.Controllers.Tests
{
    [TestClass()]
    public class CompetenceControllerTests : ControllerTest
    {
        [TestMethod()]
        public void GetCompetencesTest()
        {
            var controller = GetCompetenceController();
            var result = GetResultFromRequest<GroupedCompetencesResponse, CompetenceController>(controller, (ctrl) => { return ctrl.GetCompetences(); });

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(5, result.First().Competences.Count);
            Assert.AreEqual("Developer", result.First().Business.Name);
        }

        private CompetenceController GetCompetenceController()
        {
            return new CompetenceController(Scope.Resolve<ICompetenceServant>(), Scope.Resolve<IBusinessServant>());
        }
    }
}