using LitChat.Blazor.Models;
using LitChat.Blazor.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.Blazor.Services
{
    public class FriendService : IFriendService
    {
        private readonly IHttpServiceGeneric _httpService;

        public FriendService(IHttpServiceGeneric httpService)
        {
            _httpService = httpService;
        }

        public async Task Approved(FriendRequest approvedFriend) => await _httpService.Post("api/Friend/Approved", approvedFriend);

        public async Task CreateRequestToFriend(FriendRequest request) => await _httpService.Post("api/Friend/Request", request);

        public async Task DeleteFriend(int friendId) => await _httpService.Delete($"api/Friend/{friendId}");

        public async Task<List<Friend>> FriendsList(int accountId) => await _httpService.GetAll<Friend>($"api/Friend/FriendsList/{accountId}");

        public async Task<Friend> GetFriend(int friendId) => await _httpService.Get<Friend>($"api/Friend/GetFriend/{friendId}");

        public async Task<List<Friend>> PendingRequests(int accountId) => await _httpService.GetAll<Friend>($"api/Friend/PendingRequests/{accountId}");

        public async Task Rejected(FriendRequest rejectedFriend) => await _httpService.Post("api/Friend/Rejected", rejectedFriend);

        public async Task<List<Friend>> RejectedRequests(int accountId) => await _httpService.GetAll<Friend>($"api/Friend/RejectedRequest/{accountId}");
    }
}
