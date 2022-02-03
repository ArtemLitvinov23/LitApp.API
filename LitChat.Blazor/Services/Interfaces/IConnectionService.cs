using LitChat.Blazor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.Blazor.Services.Interfaces
{
    public interface IConnectionService
    {
        Task<Connections> GetStatusUserAsync(string UserId);
        Task<List<Connections>> GetAllStatusAsync();
        Task CloseConnection(string userId);
    }
}
