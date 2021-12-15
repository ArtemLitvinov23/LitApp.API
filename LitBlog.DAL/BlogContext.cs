using LitBlog.DAL.Models;
using LitChat.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LitBlog.DAL
{
    public class BlogContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<FavoritesList> FavoritesUsers { get; set; } 
        public DbSet<ChatMessages> Messages { get; set; }
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavoritesList>(x=>
             x.HasOne(d=>d.Account)
            .WithMany(d=>d.Favorites)
            .HasForeignKey(d=>d.AccountId)
            .OnDelete(DeleteBehavior.Cascade));
           
            modelBuilder.Entity<ChatMessages>(entity =>
            {
                entity.HasOne(p => p.FromUser)
                .WithMany(p => p.MessagesFromUser)
                .HasForeignKey(d => d.FromUserId)
                .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(p => p.ToUser)
                .WithMany(p => p.MessagesToUser)
                .HasForeignKey(d => d.ToUserId)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
