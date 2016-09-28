using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Migree.Api.Models.Requests;
using Migree.Api.Models.Responses;
using Migree.Api.Providers;
using Migree.Core.Interfaces;
using Migree.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            var otherUser = GetMockModels<User>().First(x => x.UserType == Core.Definitions.UserType.NeedsHelp);

            var resultStatusCode = await PostMessageAsync(otherUser.Id, "Just posting a message");
            Assert.AreEqual(HttpStatusCode.Accepted, resultStatusCode);
        }

        [TestMethod()]
        public async Task GetMessageThreadsAsyncTest()
        {
            SetUserAsLoggedIn(Core.Definitions.UserType.Helper);
            var otherUser = GetMockModels<User>().First(x => x.UserType == Core.Definitions.UserType.NeedsHelp);

            await PostMessageAsync(otherUser.Id, "A first message");
            await PostMessageAsync(otherUser.Id, "A second message");

            var controller = GetMessageController();
            var result = GetResultFromRequest<IEnumerable<MessageThreadResponse>, MessageController>(controller, (ctrl) => { return ctrl.GetMessageThreads(); }).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("A second message", result[0].LatestMessageContent);
            Assert.AreEqual(otherUser.Id, result[0].OtherUserId);
            Assert.AreEqual(false, result[0].IsRead);
        }

        [TestMethod()]
        public async Task GetMessageThreadAsyncTest()
        {
            var otherUser = GetMockModels<User>().First(x => x.UserType == Core.Definitions.UserType.NeedsHelp);

            await PostMessageAsync(otherUser.Id, "A primary message");
            await PostMessageAsync(otherUser.Id, "A secondary message");

            var controller = GetMessageController();
            var result = GetResultFromRequest<MessageResponse, MessageController>(controller, (ctrl) => { return ctrl.GetMessageThread(otherUser.Id); });

            Assert.AreEqual(2, result.Messages.Count);
            Assert.AreEqual("Second SecondLast", result.User.FullName);
        }

        private async Task<HttpStatusCode> PostMessageAsync(Guid otherUserId, string message)
        {
            var controller = GetMessageController();

            var messageRequest = new PostMessageRequest
            {
                Message = message,
                ReceiverUserId = otherUserId
            };

            SetRequest(ref controller);
            var result = await controller.PostMessageAsync(messageRequest);
            return result.StatusCode;
        }

        private MessageController GetMessageController()
        {
            return new MessageController(Scope.Resolve<IMessageServant>(), Scope.Resolve<IUserServant>(), Scope.Resolve<ILanguageServant>(), Scope.Resolve<ISessionProvider>());
        }
    }
}