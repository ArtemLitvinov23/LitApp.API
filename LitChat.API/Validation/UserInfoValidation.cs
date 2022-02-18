using FluentValidation;
using LitChat.API.Models;

namespace LitChat.API.Validators
{
    public class UserInfoValidation : AbstractValidator<UserInfoViewModel>
    {
        public UserInfoValidation()
        {
            RuleFor(x => x.Description).MaximumLength(100);
        }
    }
}
