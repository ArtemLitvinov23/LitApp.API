using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;

namespace LitBlog.Chat.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
    }
}
