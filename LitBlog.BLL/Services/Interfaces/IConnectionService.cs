using LitChat.BLL.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.BLL.Services.Interfaces
{
    public interface IConnectionService
    {
        Task<IEnumerable<ConnectionsDto>> GetAllClientsAsync();
        Task<ConnectionsDto> GetClientByUserIdAsync(int UserId);
        Task CreateConnectionAsync(ConnectionsDto connections);
        Task DeleteConnectionAsync(string ConnectionId);
        Task UpdateConnection(ConnectionsDto connections);
    }
}
