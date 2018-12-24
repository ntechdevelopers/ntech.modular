using System.Threading.Tasks;

namespace Ntech.Modules.Core.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
