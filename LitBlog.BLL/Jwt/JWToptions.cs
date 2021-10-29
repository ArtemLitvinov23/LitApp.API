namespace LitBlog.BLL.Jwt
{
    public class JWToptions
    {
        public string Secret { get; set; }
        public int TokenLifeTime { get; set; }
        public int RefreshTokenTTL { get; set; }
    }
}
