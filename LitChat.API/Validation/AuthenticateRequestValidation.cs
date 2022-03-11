using FluentValidation;
using LitChat.API.Models;

namespace LitChat.API.Validation
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
