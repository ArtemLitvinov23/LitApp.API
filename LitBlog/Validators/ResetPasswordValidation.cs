using FluentValidation;
using LitBlog.API.Models;

namespace LitChat.API.Validators
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
