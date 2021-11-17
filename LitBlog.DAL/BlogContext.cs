using LitBlog.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LitBlog.DAL
{
    public class BlogContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<FavoritesList> Favorites { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ChatMessage>(entity =>
            {
                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.ChatMessagesFromUsers)
                    .HasForeignKey(d => d.FromUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.ChatMessagesToUsers)
                    .HasForeignKey(d => d.ToUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
