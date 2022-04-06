namespace LitApp.PL.Models
{
    public class FavoritesListResponseViewModel
    {
        public int OwnerAccountId { get; set; }

        public int FavoriteUserAccountId { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsDeleted { get; set; }
    }
}
