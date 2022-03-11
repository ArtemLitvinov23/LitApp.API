using FluentValidation;
using LitChat.API.Models;

namespace LitChat.API.Validation
{
    public class RegistrationAccountValidation : AbstractValidator<AccountRegisterViewModel>
    {
        public RegistrationAccountValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2).WithMessage("Name must be at least 2 characters");
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(2).WithMessage("Last name must be at least 2 characters");
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$").WithMessage("Minimum eight characters, at least one letter and one number");
            RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(x => x.Password).WithMessage("Password does not match");
            RuleFor(x => x.AcceptTerms).Equal(true);
        }
    }
}
