using System.Collections.Generic;

namespace Migree.Core.Interfaces.Models
{
    public interface IClient
    {
        IDictionary<string, string> Home { get; }
        IDictionary<string, string> Login { get; }
        IDictionary<string, string> ResetPassword { get; }
        IDictionary<string, string> FinishPasswordReset { get; }
        IDictionary<string, string> NotFound { get; }
        IDictionary<string, string> ThankYou { get; }
        IDictionary<string, string> Message { get; }
        IDictionary<string, string> Messages { get; }
        IDictionary<string, string> Register { get; }
        IDictionary<string, string> Matches { get; }
    }
}
