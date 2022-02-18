using LitChat.DAL.Models;
using LitChat.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LitChat.DAL.Repositories
{
    public class FavoritesRepository : IFavoritesRepository
    {
        private readonly BlogContext _blogContext;
        public FavoritesRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task AddUserToFavorite(FavoritesList user)
        {
            await _blogContext.FavoritesUsers.AddAsync(user);
            await _blogContext.SaveChangesAsync();
        }

        public async Task<List<FavoritesList>> GetAllFavoriteForAccountUser(int id) => await _blogContext.FavoritesUsers.Where(x=>x.OwnerAccountId==id).ToListAsync();

        public async Task RemoveMeFromFavorite(int id)
        {
            var account = await _blogContext.FavoritesUsers.Where(x => x.FavoriteUserAccountId == id).ToListAsync();
            _blogContext.RemoveRange(account);
            await _blogContext.SaveChangesAsync();
        }

        public IQueryable<FavoritesList> GetAllFavoriteUser() =>  _blogContext.FavoritesUsers.AsQueryable();

        public async Task<FavoritesList> GetFavoriteUserById(int favoriteUserAccountId) => await _blogContext.FavoritesUsers.FirstOrDefaultAsync(x=>x.FavoriteUserAccountId == favoriteUserAccountId);

        public async Task RemoveUserFromFavorite(int id)
        {
            var user = await _blogContext.FavoritesUsers.FirstOrDefaultAsync(x => x.FavoriteUserAccountId==id);
            if (user != null)
                _blogContext.Remove(user);
            await _blogContext.SaveChangesAsync();
        }
    }
}
