using Migree.Api.Providers;
using System;

namespace Migree.ApiTests.Mocks
{
    public class MockSessionProvider : ISessionProvider
    {
        public Guid CurrentUserId { get; set; }
    }
}
