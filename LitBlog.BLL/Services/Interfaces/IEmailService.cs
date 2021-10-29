using System.Threading.Tasks;
using LitBlog.BLL.ModelsDto;

namespace LitBlog.BLL.Services
{
    public interface IEmailService
    {
        Task Send(string to, string subject, string html, string from = null);

        Task SendVerificationEmail(AccountDto account, string origin);

        Task SendAlreadyRegisteredEmail(string email, string origin);

        Task SendPasswordResetEmail(AccountDto account, string origin);
    }
}