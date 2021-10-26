using LitBlog.BLL.ModelsDto;

namespace LitBlog.BLL.Services
{
    public interface IEmailService
    {
        void Send(string to, string subject, string html, string from = null);

        void SendVerificationEmail(AccountDto account, string origin);

        void SendAlreadyRegisteredEmail(AccountDto account, string origin);

        void SendPasswordResetEmail(AccountDto account, string origin);
    }
}