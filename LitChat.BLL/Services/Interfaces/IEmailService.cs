using System.Threading.Tasks;
using LitChat.BLL.ModelsDto;

namespace LitChat.BLL.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendAsync(string to, string subject, string html, string from = null);

        Task<bool> SendVerificationEmailAsync(AccountDto account, string origin);

        Task SendAlreadyRegisteredEmailAsync(string email, string origin);

        Task SendPasswordResetEmailAsync(AccountDto account, string origin);
    }
}