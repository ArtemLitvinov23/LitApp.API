using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LitBlog.BLL.Settings
{
    public class EmailTemplate
    {
        public string VerifyEmailUrl { get; set; }
        public string VerificationEmailMessageSuccess { get; set; }
        public string VerificationEmailMessageFailed { get; set; }
        public string VerificationSubject { get; set; }
        public string VerificationHtml { get; set; }

        public string RegisteredEmailUrl { get; set; }
        public string RegisteredEmailMessageSuccess { get; set; }
        public string RegisteredEmailMessageFailed { get; set; }
        public string RegisteredSubject { get; set; }
        public string RegisteredHtml { get; set; }

        public string ResetPasswordUrl { get; set; }
        public string ResetPasswordSuccess { get; set; }
        public string ResetPasswordFailed { get; set; }
        public string ResetPasswordSubject { get; set; }
        public string ResetPasswordHtml { get; set; }
    }
}
