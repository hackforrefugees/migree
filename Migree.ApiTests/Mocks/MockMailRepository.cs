using Migree.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Migree.ApiTests.Mocks
{
    public class MockMailRepository : IMailRepository
    {
        public List<string> MailSent { get; } = new List<string>();

        public Task SendFinishedPasswordResetAsync(string email)
        {
            MailSent.Add($"SendFinishedPasswordResetAsync({email})");            
            return Task.Delay(1);
        }

        public Task SendInitPasswordResetAsync(string email, Guid userId, long passwordResetVerificationKey)
        {
            MailSent.Add($"SendInitPasswordResetAsync({email}, {userId}, {passwordResetVerificationKey})");
            return Task.Delay(1);
        }

        public Task SendMessageMailAsync(Guid creatorUserId, Guid receiverUserId, string message)
        {
            MailSent.Add($"SendFinishedPasswordResetAsync({creatorUserId}, {receiverUserId}, {message}");
            return Task.Delay(1);
        }

        public Task SendRegisterMailAsync(string email, string fullName)
        {
            MailSent.Add($"SendFinishedPasswordResetAsync({email}, {fullName})");
            return Task.Delay(1);
        }
    }
}
