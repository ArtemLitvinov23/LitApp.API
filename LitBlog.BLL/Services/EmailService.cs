using LitBlog.BLL.ModelsDto;
using LitBlog.BLL.Settings;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace LitBlog.BLL.Services
{
    public class EmailService:IEmailService
    {
        private readonly AppSettings _appSettings;
        private readonly EmailTemplate _emailTemplate;
        public EmailService(IOptions<AppSettings> settings, IOptions<EmailTemplate> emailTemplate)
        {
            _appSettings = settings.Value;
            _emailTemplate = emailTemplate.Value;
        }
        public void Send(string to, string subject, string html, string @from = null)
        {

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from?? _appSettings.EmailFrom));
            email.To.Add((MailboxAddress.Parse(to)));
            email.Subject = subject;
            email.Body = new TextPart((TextFormat.Html)) {Text = html};

            using var smtp = new SmtpClient();
            smtp.ConnectAsync(_appSettings.SmtpHost, _appSettings.SmtpPort, SecureSocketOptions.StartTls);
            smtp.AuthenticateAsync(_appSettings.SmtpUser, _appSettings.SmtpPass);
            smtp.SendAsync(email);
            smtp.DisconnectAsync(true);
        }

        public void SendVerificationEmail(AccountDto account, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var verifyUrl = _emailTemplate.VerifyEmailUrl;
                message = _emailTemplate.VerificationEmailMessageSuccess;
            }
            else
                message = _emailTemplate.VerificationEmailMessageFailed;

            Send(
                to: account.Email,
                subject: _emailTemplate.VerificationSubject,
                html: _emailTemplate.VerificationHtml
                );
        }

        public void SendAlreadyRegisteredEmail(AccountDto account, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
                message = _emailTemplate.RegisteredEmailMessageSuccess;
            else
                message = _emailTemplate.RegisteredEmailMessageFailed;

            Send(
                to: account.Email,
                subject: _emailTemplate.RegisteredSubject,
                html: _emailTemplate.RegisteredHtml
            );
        }

        public void SendPasswordResetEmail(AccountDto account, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
                message = _emailTemplate.ResetPasswordSuccess;
            else
                message = _emailTemplate.ResetPasswordFailed;

            Send(
                to: account.Email,
                subject: _emailTemplate.ResetPasswordSubject,
                html: _emailTemplate.ResetPasswordHtml
            );
        }
    }
}
