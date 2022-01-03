using FluentValidation;
using LitBlog.API.Models;

namespace LitChat.API.Validators
{
    public class AuthenticateRequestValidation : AbstractValidator<AuthenticateRequestViewModel>
    {
        public AuthenticateRequestValidation()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        }
    }
}
