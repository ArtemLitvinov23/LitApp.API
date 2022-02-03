namespace LitChat.Blazor.Models
{
    public class FavoritesListResponse
    {
        public int Id { get; set; }
        public int OwnerAccountId { get; set; }
        public int FavoriteUserAccountId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
