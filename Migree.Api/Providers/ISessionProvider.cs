using System;

namespace Migree.Api.Providers
{
    public interface ISessionProvider
    {
        Guid CurrentUserId { get; }
    }
}
