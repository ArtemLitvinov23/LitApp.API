using LitApp.BLL.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitApp.BLL.Services.Interfaces
{
    public interface IConnectionService
    {
        Task<IEnumerable<ConnectionsResponseDto>> GetAllClientsAsync();
        Task<ConnectionsResponseDto> GetConnectionForUserAsync(int accountId);
        Task<ConnectionsResponseDto> GetConnectionById(int connectionId);
        Task<ConnectionsResponseDto> GetClientByUserIdAsync(int UserId);
        Task CreateConnectionAsync(ConnectionsDto connections);
        Task CloseConnection(int accountId);
        Task DeleteConnectionAsync(int userId);
        Task UpdateConnection(ConnectionsDto connections);
    }
}
