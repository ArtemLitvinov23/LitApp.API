using FluentValidation;
using LitApp.PL.Models;

namespace LitApp.PL.Validation
{
    public class VerifyTokenValidation : AbstractValidator<VerifyRequestViewModel>
    {
        public VerifyTokenValidation()
        {
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}
