namespace LitChat.BLL.Jwt.Options
{
    public class JWtOptions
    {
        public string Secret { get; set; }
        public int TokenLifeTime { get; set; }
        public int RefreshTokenTTL { get; set; }
    }
}
