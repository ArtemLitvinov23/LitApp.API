using FluentValidation;
using LitChat.API.Models;

namespace LitChat.API.Validators
{
    public class UpdateUserValidation : AbstractValidator<UpdateAccountViewModel>
    {
        public UpdateUserValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2).WithMessage("Name must be at least 2 characters");
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(2).WithMessage("Last name must be at least 2 characters");
            RuleFor(x => x.Description).MaximumLength(100);
        }
    }
}
