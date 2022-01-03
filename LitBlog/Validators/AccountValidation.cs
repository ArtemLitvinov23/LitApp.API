using FluentValidation;
using LitChat.API.Models;

namespace LitChat.API.Validators
{
    public class AccountValidation : AbstractValidator<AccountViewModel>
    {
        public AccountValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2);
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(2);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
            RuleFor(x => x.Description).MaximumLength(100);
        }
    }
}
