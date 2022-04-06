using FluentValidation;
using LitApp.PL.Models;

namespace LitApp.PL.Validation
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
