using Migree.Core.Interfaces;
using SendGrid;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Migree.Core.Repositories
{
    public class SendGridMailRepository : IMailRepository
    {
        private ISettingsServant SettingsServant { get; }
        public SendGridMailRepository(ISettingsServant settingsServant)
        {
            SettingsServant = settingsServant;
        }
        public async Task SendMailAsync(string subject, string message, string mailTo, string mailFrom, string fromName, string replyTo)
        {
            var mailMessage = new SendGridMessage();
            mailMessage.AddTo(mailTo);
            mailMessage.From = new MailAddress(mailFrom, fromName);
            mailMessage.Subject = subject;
            mailMessage.Html = message;

            if (string.IsNullOrWhiteSpace( replyTo ))
            {
                mailMessage.ReplyTo = new List<MailAddress> { new MailAddress(replyTo, fromName) }.ToArray();
            }            

            var transportREST = new Web(SettingsServant.SendGridCredentials);
            await transportREST.DeliverAsync(mailMessage);
        }
    }

   
}
