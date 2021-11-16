namespace LitBlog.BLL.Jwt
{
    public class JWtOptions
    {
        public string Secret { get; set; }
        public int TokenLifeTime { get; set; }
        public int RefreshTokenTTL { get; set; }
    }
}
