using System;
using System.Threading.Tasks;

namespace Migree.Core.Interfaces
{
    public interface IMailRepository
    {
        Task SendMessageMailAsync(Guid creatorUserId, Guid receiverUserId, string message);
        Task SendRegisterMailAsync(string email, string fullName);
        Task SendInitPasswordResetAsync(string email, Guid userId, long passwordResetVerificationKey);
        Task SendFinishedPasswordResetAsync(string email);
    }
}
