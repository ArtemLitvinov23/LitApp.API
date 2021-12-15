namespace LitChat.API.Models
{
    public class FavoritesListResponseViewModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
