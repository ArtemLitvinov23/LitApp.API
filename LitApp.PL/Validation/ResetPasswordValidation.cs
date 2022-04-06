using FluentValidation;
using LitApp.PL.Models;

namespace LitApp.PL.Validation
{
    public class ResetPasswordValidation : AbstractValidator<ResetPasswordRequestViewModel>
    {
        public ResetPasswordValidation()
        {
            RuleFor(x => x.Token).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
            RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(x => x.Password).WithMessage("Password does not match");
        }
    }
}
