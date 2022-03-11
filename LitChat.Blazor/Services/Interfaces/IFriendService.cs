using LitChat.Blazor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.Blazor.Services.Interfaces
{
    public interface IFriendService
    {
        Task<List<Friend>> FriendsList(int accountId);
        Task<List<Friend>> RejectedRequests(int accountId);
        Task<List<Friend>> PendingRequests(int accountId);
        Task<Friend> GetFriend(int friendId);
        Task Approved(FriendRequest approvedFriend);
        Task Rejected(FriendRequest rejectedFriend);
        Task CreateRequestToFriend(FriendRequest request);
        Task DeleteFriend(int friendId);
    }
}
