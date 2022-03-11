using FluentValidation;
using LitChat.API.Models;

namespace LitChat.API.Validation
{
    public class AccountValidation : AbstractValidator<AccountViewModel>
    {
        public AccountValidation()
        {
            RuleFor(x => x.Profile.FirstName).NotEmpty().MinimumLength(2);
            RuleFor(x => x.Profile.LastName).NotEmpty().MinimumLength(2);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
            RuleFor(x => x.Profile.Description).MaximumLength(100);
        }
    }
}
