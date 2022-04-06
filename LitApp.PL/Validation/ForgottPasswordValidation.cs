using FluentValidation;
using LitApp.PL.Models;

namespace LitApp.PL.Validation
{
    public class ForgottPasswordValidation : AbstractValidator<ForgotPasswordRequestViewModel>
    {
        public ForgottPasswordValidation()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
