using System.Threading.Tasks;

namespace Migree.Core.Interfaces
{
    public interface IMailRepository
    {
        Task SendMailAsync(string subject, string message, string mailTo, string mailFrom, string fromName, string replyTo);
    }
}
