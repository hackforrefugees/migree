using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Migree.Api.Models.Responses;
using Migree.Api.Providers;
using Migree.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Migree.Api.Controllers.Tests
{
    [TestClass()]
    public class MatchesControllerTests : ControllerTest
    {
        [TestMethod()]
        public void GetMatchesHelperTest()
        {
            SetUserAsLoggedIn(Core.Definitions.UserType.Helper);
            var controller = GetMatchesController();
            var result = GetResultFromRequest<IEnumerable<UserResponse>, MatchesController>(controller, (ctrl) => { return ctrl.GetMatches(); }).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Second SecondLast", result.First().FullName);
        }

        [TestMethod()]
        public void GetMatchesNeedHelpTest()
        {
            SetUserAsLoggedIn(Core.Definitions.UserType.NeedsHelp);
            var controller = GetMatchesController();
            var result = GetResultFromRequest<IEnumerable<UserResponse>, MatchesController>(controller, (ctrl) => { return ctrl.GetMatches(); }).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("First FirstLast", result.First().FullName);
        }

        private MatchesController GetMatchesController()
        {
            return new MatchesController(Scope.Resolve<ICompetenceServant>(), Scope.Resolve<IUserServant>(), Scope.Resolve<ILanguageServant>(), Scope.Resolve<ISessionProvider>());
        }
    }
}