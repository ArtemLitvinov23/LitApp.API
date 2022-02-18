using LitChat.BLL.Exceptions;
using LitChat.BLL.ModelsDto;
using LitChat.BLL.Services.Interfaces;
using LitChat.BLL.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Threading.Tasks;

namespace LitChat.BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _appSettings;
        public EmailService(IOptions<EmailSettings> settings)
        {
            _appSettings = settings.Value;
        }
        public async Task SendAsync(string to, string subject, string html, string from = null)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(from ?? _appSettings.EmailFrom));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) { Text = html };


                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_appSettings.SmtpHost, _appSettings.SmtpPort, true);
                await smtp.AuthenticateAsync(_appSettings.SmtpUser, _appSettings.SmtpPass);
                await smtp.SendAsync(email);

                await smtp.DisconnectAsync(true);
            }
            catch (Exception e)
            {
                throw new InternalServerException(e.Message);
            }

        }

        public async Task SendVerificationEmailAsync(AccountDto account, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var verifyUrl = $"{origin}/account/verify/{account.VerificationToken}";
                message = $@"<p>Please click the below link to verify your email address:</p>
                             <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>";
            }
            else
            {
                var url = "https://localhost:44311/";
                var verifyUrlWithoutOrigin = $"{url}/account/verify/{account.VerificationToken}";
                message = $@"<p>Please use the below token to verify your email address with the  api route:</p>
                              <p><a href=""{verifyUrlWithoutOrigin}"">{verifyUrlWithoutOrigin}</a></p>";
            }

            await SendAsync(account.Email, "Sign-up Verification API - Verify Email",
                $@"<h4>Verify Email</h4>
                     <p>Thanks for registering!</p>
                      {message}"
                     );
        }

        public async Task SendAlreadyRegisteredEmailAsync(string email, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
                message = $@"<p>If you don't know your password please visit the <a href=""{origin}/account/forgot-password"">forgot password</a> page.</p>";
            else
                message = "<p>If you don't know your password you can reset it via the <code>/account/forgot-password</code> api route.</p>";

            await SendAsync(
                email,
                "Sign-up Verification API - Email Already Registered",
                $@"<h4>Email Already Registered</h4>
                         <p>Your email <strong>{email}</strong> is already registered.</p>
                         {message}"
            );
        }

        public async Task SendPasswordResetEmailAsync(AccountDto account, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var resetUrl = $"{origin}/account/reset-password?token={account.ResetToken}";
                message = $@"<p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
                             <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
            }
            else
                message = $@"<p>Please use the below token to reset your password with the <code>/account/reset-password</code> api route:</p>
                             <p><code>{account.ResetToken}</code></p>";

            await SendAsync(
                account.Email,
                 "Sign-up Verification API - Reset Password",
                 $@"<h4>Reset Password Email</h4>
                         {message}"
            );
        }
    }
}
