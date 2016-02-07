using Migree.Core.Interfaces;
using Migree.Core.Models;
using Migree.Core.Models.Language;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Migree.Core.Repositories
{
    public class SendGridMailRepository : IMailRepository
    {
        private ISettingsServant SettingsServant { get; }
        private IDataRepository DataRepository { get; }
        private ILanguageServant LanguageServant { get; }
        public SendGridMailRepository(ISettingsServant settingsServant, IDataRepository dataRepository, ILanguageServant languageServant)
        {
            SettingsServant = settingsServant;
            DataRepository = dataRepository;
            LanguageServant = languageServant;
        }

        public async Task SendMessageMailAsync(Guid creatorUserId, Guid receiverUserId, string message)
        {
            var language = LanguageServant.Get<MessageMail>();
            var creatorUser = DataRepository.GetAll<User>(p => p.RowKey.Equals(User.GetRowKey(creatorUserId))).FirstOrDefault();
            var receiverUser = DataRepository.GetAll<User>(p => p.RowKey.Equals(User.GetRowKey(receiverUserId))).FirstOrDefault();

            var subject = LanguageServant.GetString(language.Subject, creatorUser.FullName);
            message = LanguageServant.GetString(language.Message, message, creatorUser.Email, creatorUser.FullName);

            await SendMailAsync(
                subject,
                message,
                receiverUser.Email,
                LanguageServant.GetString(language.FromMail),
                LanguageServant.GetString(language.FromName, creatorUser.FullName),
                creatorUser.Email);
        }

        public async Task SendRegisterMailAsync(string email, string fullName)
        {
            var language = LanguageServant.Get<RegistrationMail>();

            await SendMailAsync(
                language.Subject,
                LanguageServant.GetString(language.Message, fullName),
                email,
                language.FromMail,
                language.FromName);
        }

        public async Task SendInitPasswordResetAsync(string email, Guid userId, long passwordResetVerificationKey)
        {
            var language = LanguageServant.Get<InitPasswordResetMail>();

            await SendMailAsync(
                language.Subject,
                LanguageServant.GetString(language.Message, userId, passwordResetVerificationKey),
                email,
                language.FromMail,
                language.FromName);
        }

        public async Task SendFinishedPasswordResetAsync(string email)
        {
            var language = LanguageServant.Get<FinishedPasswordResetMail>();

            await SendMailAsync(
                language.Subject,
                language.Message,
                email,
                language.FromMail,
                language.FromName);
        }

        private async Task SendMailAsync(string subject, string message, string mailTo, string mailFrom, string fromName, string replyTo = "")
        {
            return;

            var mailMessage = new SendGridMessage();
            mailMessage.AddTo(mailTo);
            mailMessage.From = new MailAddress(mailFrom, fromName);
            mailMessage.Subject = subject;
            mailMessage.Text = message;

            if (!string.IsNullOrWhiteSpace(replyTo))
            {
                mailMessage.ReplyTo = new List<MailAddress> { new MailAddress(replyTo, fromName) }.ToArray();
            }

            var transportREST = new Web(SettingsServant.SendGridCredentials);
            await transportREST.DeliverAsync(mailMessage);
        }
    }


}
