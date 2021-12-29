using LitChat.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.DAL.Repositories.Interfaces
{
    public interface IConnectionRepository
    {
        Task<IEnumerable<Connections>> GetAllClients();
        Task<Connections> GetExistsConnectionAsync(int userAccount);
        Task<Connections> GetClientById(int UserId);
        Task CreateConnection(Connections connections);
        Task UpdateConnection(Connections connections);
        Task DeleteConnection(string ConnectionId);
    }
}
