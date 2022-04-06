using LitApp.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitApp.DAL.Repositories.Interfaces
{
    public interface IConnectionRepository
    {
        Task<IEnumerable<Connections>> GetAllClients();
        Task<Connections> GetConnectionForUserAsync(int userAccount);
        Task<Connections> GetClientById(int UserId);
        Task<Connections> GetConnectionsById(int connectionId);
        Task CreateConnection(Connections connections);
        Task UpdateConnection(Connections connections);
        Task DeleteConnection(string ConnectionId);
    }
}
