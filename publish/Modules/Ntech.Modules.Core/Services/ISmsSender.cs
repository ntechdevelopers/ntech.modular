using System.Threading.Tasks;

namespace Ntech.Modules.Core.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
