using LitBlazor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitBlazor.Services.Interfaces
{
    public interface IConnectionService
    {
        Task<Connections> GetStatusFavoriteUserAsync(string favoriteUserId);
        Task<IEnumerable<Connections>> GetAllStatusAsync();
    }
}
