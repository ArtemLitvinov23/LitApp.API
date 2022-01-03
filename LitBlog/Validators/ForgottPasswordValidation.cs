using FluentValidation;
using LitBlog.API.Models;
namespace LitChat.API.Validators
{
    public class ForgottPasswordValidation: AbstractValidator<ForgotPasswordRequestViewModel>
    {
        public ForgottPasswordValidation()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
