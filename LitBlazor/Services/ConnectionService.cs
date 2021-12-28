using LitBlazor.Models;
using LitBlazor.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitBlazor.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly IHttpServiceGeneric _httpServiceGeneric;

        public ConnectionService(IHttpServiceGeneric httpServiceGeneric)
        {
            _httpServiceGeneric = httpServiceGeneric;
        }
        public async Task<IEnumerable<Connections>> GetAllStatusAsync() => await _httpServiceGeneric.GetAll<Connections>("api/Online");
        public async Task<Connections> GetStatusFavoriteUserAsync(string favoriteUserId) => await _httpServiceGeneric.Get<Connections>($"api/Online/{favoriteUserId}");
    }
}
