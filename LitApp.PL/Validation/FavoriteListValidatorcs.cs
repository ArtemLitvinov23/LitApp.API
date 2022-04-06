using FluentValidation;
using LitApp.PL.Models;

namespace LitApp.PL.Validation
{
    public class FavoriteListValidatorcs : AbstractValidator<FavoritesListViewModel>
    {
        public FavoriteListValidatorcs()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
