using LitBlazor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitBlazor.Services.Interfaces
{
    public interface IConnectionService
    {
        Task<Connections> GetStatusUserAsync(string UserId);
        Task<List<Connections>> GetAllStatusAsync();
        Task CloseConnection(string userId);
    }
}
