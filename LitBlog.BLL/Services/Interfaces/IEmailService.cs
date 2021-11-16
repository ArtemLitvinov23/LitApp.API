using System.Threading.Tasks;
using LitBlog.BLL.ModelsDto;

namespace LitBlog.BLL.Services
{
    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string html, string from = null);

        Task SendVerificationEmailAsync(AccountDto account, string origin);

        Task SendAlreadyRegisteredEmailAsync(string email, string origin);

        Task SendPasswordResetEmailAsync(AccountDto account, string origin);
    }
}