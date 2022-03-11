using FluentValidation;
using LitChat.API.Models;

namespace LitChat.API.Validation
{
    public class VerifyTokenValidation : AbstractValidator<VerifyRequestViewModel>
    {
        public VerifyTokenValidation()
        {
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}
