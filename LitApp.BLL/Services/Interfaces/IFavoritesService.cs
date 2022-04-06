using LitApp.BLL.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitApp.BLL.Services.Interfaces
{
    public interface IFavoritesService
    {
        Task<List<FavoritesListResponseDto>> GetAllFavoriteUserAsync();
        Task<FavoritesListResponseDto> GetFavoriteUserByAccountIdAsync(int id);
        Task<List<FavoritesListResponseDto>> GetAllFavoriteUserForAccountAsync(int id);
        Task AddUserToFavoriteAsync(FavoritesListDto user);
        Task RemoveUserFromFavoriteAsync(int id);
    }
}
