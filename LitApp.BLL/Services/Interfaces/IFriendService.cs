using LitApp.BLL.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitApp.BLL.Services.Interfaces
{
    public interface IFriendService
    {
        Task SendRequestToUserAsync(FriendRequestDto friendRequestDto);
        Task<List<FriendDto>> GetAllApprovedFriendsAsync(int accountId);
        Task<List<FriendDto>> GetAllPendingRequestsAsync(int accountId);
        Task<List<FriendDto>> GetAllRejectedRequestsAsync(int accountId);
        Task<FriendDto> GetFriendById(int id);
        Task ApprovedUserAsync(FriendRequestDto friendRequestDto);
        Task RejectUserAsync(FriendRequestDto friendRequestDto);
        Task DeleteUserFromFriends(int friendId);
    }
}
