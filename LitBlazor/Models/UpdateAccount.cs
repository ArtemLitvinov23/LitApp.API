namespace LitBlazor.Models
{
    public class UpdateAccount
    {
        public string UserName { get; set; }
        
        public string LastName { get; set; }

        public UpdateAccount(){}

        public UpdateAccount(Account account)
        {
            UserName = account.UserName;
            LastName = account.LastName;
        }
    }
}
