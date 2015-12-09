using Migree.Core.Interfaces;
using SendGrid;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using System;
using Migree.Core.Models;
using System.Linq;
using Migree.Core.Models.Language;

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
            var language = LanguageServant.Get<SendMessageMail>();
            var creatorUser = DataRepository.GetAll<User>(p => p.RowKey.Equals(User.GetRowKey(creatorUserId))).FirstOrDefault();
            var receiverUser = DataRepository.GetAll<User>(p => p.RowKey.Equals(User.GetRowKey(receiverUserId))).FirstOrDefault();

            var subject = LanguageServant.Get(language.Subject, creatorUser.FullName);
            message = LanguageServant.Get(language.Message, message, creatorUser.Email, creatorUser.FullName);

            await SendMailAsync(
                subject,
                message,
                receiverUser.Email,
                LanguageServant.Get(language.FromMail),
                LanguageServant.Get(language.FromName, creatorUser.FullName),
                creatorUser.Email);
        }

        private async Task SendMailAsync(string subject, string message, string mailTo, string mailFrom, string fromName, string replyTo)
        {
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
