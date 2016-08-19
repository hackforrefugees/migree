using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Migree.Api.Controllers;
using Migree.Api.Models.Requests;
using Migree.Api.Providers;
using Migree.ApiTests.Mocks;
using Migree.Core.Interfaces;
using Migree.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migree.Api.Controllers.Tests
{
    [TestClass()]
    public class MessageControllerTests : ControllerTest
    {
        [TestMethod()]
        public async Task PostMessageAsyncTest()
        {
            SetUserAsLoggedIn(Core.Definitions.UserType.Helper);
            var controller = new MessageController(Scope.Resolve<IMessageServant>(), Scope.Resolve<IUserServant>(), Scope.Resolve<ILanguageServant>(), Scope.Resolve<ISessionProvider>());

            var dataRepository = Scope.Resolve<IDataRepository>() as MockDataRepository;
            var otherUser = dataRepository.GetMockModels<User>().First(x => x.UserType == Core.Definitions.UserType.NeedsHelp);

            var message = new PostMessageRequest
            {
                Message = "Hello fellow",
                ReceiverUserId = otherUser.Id
            };

            var result = await controller.PostMessageAsync(message);
            Assert.AreEqual(System.Net.HttpStatusCode.Accepted, result.StatusCode);
        }

        [TestMethod()]
        public void GetMessageThreadsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetMessageThreadTest()
        {
            Assert.Fail();
        }
    }
}