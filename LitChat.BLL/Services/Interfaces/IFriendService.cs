using LitChat.BLL.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.BLL.Services.Interfaces
{
    public interface IFriendService
    {
        Task SendRequestToUserAsync(int senderId, int friendId);
        Task<List<FriendDto>> GetAllApprovedFriendsAsync();
        Task<List<FriendDto>> GetAllPendingRequestsAsync(int accountId);
        Task<List<FriendDto>> GetAllRejectedRequestsAsync();
        Task<FriendDto> GetFriendById(int id);
        Task ApprovedUserAsync(int senderId, int friendId);
        Task RejectUserAsync(int senderId, int friendId);
        Task DeleteUserFromFriends(int friendId);
    }
}
