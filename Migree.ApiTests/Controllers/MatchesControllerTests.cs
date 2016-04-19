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
        public void GetMatchesTest()
        {
            SetUserAsLoggedIn();
            var controller = new MatchesController(Scope.Resolve<ICompetenceServant>(), Scope.Resolve<IUserServant>(), Scope.Resolve<ILanguageServant>(), Scope.Resolve<ISessionProvider>());
            var result = GetResultFromRequest<IEnumerable<UserResponse>, MatchesController>(controller, (ctrl) => { return ctrl.GetMatches(); }).ToList();

            Assert.Fail();
        }
    }
}