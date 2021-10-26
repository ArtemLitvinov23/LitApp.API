using LitBlog.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LitBlog.DAL
{
    public class BlogContext:DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public BlogContext(DbContextOptions<BlogContext> options):base(options)
        {
        }
    }
}
