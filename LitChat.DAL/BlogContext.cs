using LitChat.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LitChat.DAL
{
    public class BlogContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<FavoritesList> FavoritesUsers { get; set; }
        public DbSet<ChatMessages> Messages { get; set; }
        public DbSet<Connections> Connections { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(x =>
            {
                x.HasOne(a => a.Profile)
                .WithOne(a => a.Account)
                .HasForeignKey<UserInfo>(a => a.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Friend>(x =>
            {
                x.HasOne(a => a.RequestBy)
                .WithMany(a => a.SentFriendsRequest)
                .HasForeignKey(a => a.RequestById)
                .OnDelete(DeleteBehavior.ClientSetNull);
                x.HasOne(a => a.RequestTo)
                .WithMany(a => a.RecievedFriendRequest)
                .HasForeignKey(a => a.RequestToId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });


            modelBuilder.Entity<FavoritesList>(x =>
            {
                x.HasOne(d => d.FavoriteAccount)
                .WithMany(d => d.Favorites)
                .HasForeignKey(d => d.FavoriteUserAccountId)
                .OnDelete(DeleteBehavior.Cascade);
                x.HasIndex(d => d.OwnerAccountId);
            });

            modelBuilder.Entity<ChatMessages>(entity =>
            {
                entity.HasOne(p => p.FromUser)
                .WithMany(p => p.MessagesFromUser)
                .HasForeignKey(d => d.FromUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(p => p.ToUser)
                .WithMany(p => p.MessagesToUser)
                .HasForeignKey(d => d.ToUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Connections>(entity =>
            {
                entity.HasOne(p => p.Account)
                .WithMany(p => p.Connections)
                .HasForeignKey(d => d.UserAccount)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
