using FluentValidation;
using LitBlog.API.Models;
namespace LitChat.API.Validators
{
    public class VerifyTokenValidation: AbstractValidator<VerifyRequestViewModel>
    {
        public VerifyTokenValidation()
        {
            RuleFor(x=>x.Token).NotEmpty();
        }
    }
}
