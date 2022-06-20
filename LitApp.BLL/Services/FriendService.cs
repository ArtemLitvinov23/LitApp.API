using AutoMapper;
using LitApp.BLL.Exceptions;
using LitApp.BLL.ModelsDto;
using LitApp.BLL.Services.Interfaces;
using LitApp.DAL.Models;
using LitApp.DAL.Models.Enum;
using LitApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LitApp.BLL.Services
{
    public class FriendService : IFriendService
    {

        private readonly IFriendRepository _friendRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        public FriendService(
            IFriendRepository friendRepository,
            IMapper mapper,
            IAccountRepository accountRepository)
        {
            _friendRepository = friendRepository;
            _mapper = mapper;
            _accountRepository = accountRepository;
        }

        public async Task ApprovedUserAsync(FriendRequestDto friendRequestDto)
        {
            var mapFriendRequest = _mapper.Map<FriendRequest>(friendRequestDto);

            var response = await _friendRepository.GetRequests(mapFriendRequest);
            if (response != null)
            {
                response.RequestTime = null;
                response.ApprovedDate = DateTime.UtcNow;
                response.DateOfRejection = null;
                response.NextRequest = null;
                response.RequestFlags = RequestFlags.Approved;
                await _friendRepository.UpdateFriendsRequestAsync(response);
            }
        }

        public async Task DeleteUserFromFriends(int friendId)
        {
            var friendAccount = await _accountRepository.GetAccountByIdAsync(friendId);

            if (friendAccount == null) throw new AppException("Account is not found");

            await _friendRepository.RemoveUserFromFriends(friendAccount.Id);
        }

        public async Task<List<FriendDto>> GetAllApprovedFriendsAsync(int accountId)
        {
            var friends = await _friendRepository.GetAllFriends()
                .Where(x => x.RequestFlags == RequestFlags.Approved)
                .ToListAsync();

            return _mapper.Map<List<FriendDto>>(friends);
        }


        public async Task<List<FriendDto>> GetAllPendingRequestsAsync(int accountId) => _mapper.Map<List<FriendDto>>(await _friendRepository.GetAllFriends().
            Where(x => x.RequestFlags == RequestFlags.Pending && x.RequestToId == accountId)
            .ToListAsync());

        public async Task<List<FriendDto>> GetAllRejectedRequestsAsync(int accountId) => _mapper.Map<List<FriendDto>>(await _friendRepository.GetAllFriends()
            .Where(x => x.RequestFlags == RequestFlags.Rejected && x.RequestToId == accountId || x.RequestById == accountId)
            .ToListAsync());

        public async Task<FriendDto> GetFriendById(int friendAccountId)
        {
            var friend = await _friendRepository.GetFriendById(friendAccountId);
            if (friend == null) return null;

            var response = _mapper.Map<FriendDto>(friend);
            return response;
        }

        public async Task RejectUserAsync(FriendRequestDto friendRequest)
        {
            var response = await GetRequest(friendRequest);

            response.RequestTime = null;
            response.RequestFlags = RequestFlags.Rejected;
            response.DateOfRejection = DateTime.UtcNow;
            response.NextRequest = DateTime.UtcNow.AddDays(2);

            await _friendRepository.UpdateFriendsRequestAsync(response);
        }

        public async Task SendRequestToUserAsync(FriendRequestDto friendRequest)
        {
            var response = await GetRequest(friendRequest);

            if (response == null)
            {
                var createRequest = new FriendDto(friendRequest.SenderId, friendRequest.RecieverId);
                var friends = _mapper.Map<Friend>(createRequest);
                await _friendRepository.AddUserToFriends(friends);
            }
            else if (response.NextRequest < DateTime.UtcNow)
            {
                response.RequestFlags = RequestFlags.Pending;
                response.RequestTime = DateTime.UtcNow;
                response.DateOfRejection = null;
                response.NextRequest = null;
                await _friendRepository.UpdateFriendsRequestAsync(response);
            }
            else
                throw new AppException($"You can't send a request earlier than {response.NextRequest}");
        }

        private async Task<Friend> GetRequest(FriendRequestDto friendRequest)
        {
            var mapFriendRequest = _mapper.Map<FriendRequest>(friendRequest);

            var response = await _friendRepository.GetRequests(mapFriendRequest);
            if (response == null)
            {
                throw new AppException("Not found");
            }
            return response;
        }
    }
}
