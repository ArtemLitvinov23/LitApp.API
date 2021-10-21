using LitBlog.BLL.ModelsDto;

namespace LitBlog.BLL.Services
{
    public interface IEmailService
    {
        void Send(string to, string subject, string html, string from = null);

        void SendVerificationEmail(AccountCreateDto account, string origin);

        void SendAlreadyRegisteredEmail(AccountCreateDto account, string origin);

        void SendPasswordResetEmail(AccountCreateDto account, string origin);
    }
}