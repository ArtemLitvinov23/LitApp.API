using LitChat.BLL.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.BLL.Services.Interfaces
{
    public interface IConnectionService
    {
        Task<IEnumerable<ConnectionsResponseDto>> GetAllClientsAsync();
        Task<ConnectionsDto> GetExistsConnectionAsync(int accountId);
        Task<ConnectionsResponseDto> GetClientByUserIdAsync(int UserId);
        Task CreateConnectionAsync(ConnectionsDto connections);
        Task CloseConnection(int accountId);
        Task DeleteConnectionAsync(int userId);
        Task UpdateConnection(ConnectionsDto connections);
    }
}
