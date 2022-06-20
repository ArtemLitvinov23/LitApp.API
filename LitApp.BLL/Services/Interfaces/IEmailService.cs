using LitApp.BLL.ModelsDto;
using LitApp.DAL.Models;
using System.Threading.Tasks;

namespace LitApp.BLL.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string html, string from = null);

        Task SendVerificationEmailAsync(Account account, string origin);

        Task SendAlreadyRegisteredEmailAsync(string email, string origin);

        Task SendPasswordResetEmailAsync(Account account, string origin);
    }
}