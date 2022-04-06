using FluentValidation;
using LitApp.PL.Models;

namespace LitApp.PL.Validation
{
    public class UserInfoValidation : AbstractValidator<UserInfoViewModel>
    {
        public UserInfoValidation()
        {
            RuleFor(x => x.Description).MaximumLength(100);
        }
    }
}
