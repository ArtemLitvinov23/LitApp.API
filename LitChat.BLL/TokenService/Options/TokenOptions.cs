namespace LitChat.BLL.Jwt.Options
{
    public class TokenOptions
    {
        public string Secret { get; set; }
        public string TokenLifeTime { get; set; }
        public string RefreshTokenTTL { get; set; }
    }
}
