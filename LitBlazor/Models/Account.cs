namespace LitBlazor.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string jwtToken { get; set; }
    }
}
