using LitApp.BLL.ModelsDto;
using LitApp.DAL.Models;
using System.Threading.Tasks;

namespace LitApp.BLL.Services.Interfaces
{
    public interface IEmailService
    {
        Task<StatusEnum> SendAsync(string to, string subject, string html, string from = null);

        Task<StatusEnum> SendVerificationEmailAsync(Account account, string origin);

        Task SendAlreadyRegisteredEmailAsync(string email, string origin);

        Task SendPasswordResetEmailAsync(Account account, string origin);
    }
}