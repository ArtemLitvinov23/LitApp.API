using LitChat.BLL.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.BLL.Services.Interfaces
{
    public interface IFavoritesService
    {
        Task<List<FavoritesListResponseDto>> GetAllFavoriteUserAsync();
        Task<FavoritesListResponseDto> GetFavoriteUserByIdAsync(int id);
        Task AddUserToFavoriteAsync(FavoritesListDto user);
        Task RemoveUserFromFavoriteAsync(int id);
    }
}
