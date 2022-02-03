using LitChat.Blazor.Models;
using LitChat.Blazor.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.Blazor.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly IHttpServiceGeneric _httpServiceGeneric;
        public ConnectionService(IHttpServiceGeneric httpServiceGeneric)
        {
            _httpServiceGeneric = httpServiceGeneric;
        }
        public async Task<List<Connections>> GetAllStatusAsync() => await _httpServiceGeneric.GetAll<Connections>("api/Online");
        public async Task<Connections> GetStatusUserAsync(string favoriteUserId) => await _httpServiceGeneric.Get<Connections>($"api/Online/{favoriteUserId}");
        public async Task CloseConnection(string userId) => await _httpServiceGeneric.Patch(userId);
    }
}
