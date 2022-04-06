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
            IMapper mapper, IAccountRepository accountRepository)
        {
            _friendRepository = friendRepository;
            _mapper = mapper;
            _accountRepository = accountRepository;
        }

        public async Task ApprovedUserAsync(int senderId, int friendId)
        {
            var senderAccount = await _accountRepository.GetAccountByIdAsync(senderId);
            var friendAccount = await _accountRepository.GetAccountByIdAsync(friendId);
            if (senderAccount is null || friendAccount is null) throw new AppException("Users is not found");

            var response = await _friendRepository.GetRequests(senderAccount, friendAccount);
            if (response != null)
            {
                response.RequestTime = null;
                response.ApprovedDate = DateTime.Now.ToLocalTime();
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

        public async Task RejectUserAsync(int senderId, int friendId)
        {
            var senderAccount = await _accountRepository.GetAccountByIdAsync(senderId);
            var friendAccount = await _accountRepository.GetAccountByIdAsync(friendId);
            if (senderAccount is null || friendAccount is null) throw new AppException("Users is not found");

            var response = await _friendRepository.GetRequests(senderAccount, friendAccount);
            if (response == null) throw new AppException("Current request is not found");

            response.RequestTime = null;
            response.RequestFlags = RequestFlags.Rejected;
            response.DateOfRejection = DateTime.Now.ToLocalTime();
            response.NextRequest = DateTime.Now.AddDays(2).ToLocalTime();
            await _friendRepository.UpdateFriendsRequestAsync(response);
        }

        public async Task SendRequestToUserAsync(int senderId, int friendId)
        {
            var senderAccount = await _accountRepository.GetAccountByIdAsync(senderId);
            var friendAccount = await _accountRepository.GetAccountByIdAsync(friendId);
            if (senderAccount is null || friendAccount is null)
            {
                throw new AppException("Users is not found");
            }

            var request = await _friendRepository.GetRequests(senderAccount, friendAccount);
            if (request == null)
            {
                var createRequest = new FriendDto()
                {
                    RequestById = senderAccount.Id,
                    RequestToId = friendAccount.Id,
                    RequestFlags = RequestFlags.Pending,
                    RequestTime = DateTime.Now.ToLocalTime()
                };
                var friends = _mapper.Map<Friend>(createRequest);
                await _friendRepository.AddUserToFriends(friends);
            }
            else if (request.NextRequest < DateTime.Now.ToLocalTime())
            {
                request.RequestFlags = RequestFlags.Pending;
                request.RequestTime = DateTime.Now.ToLocalTime();
                request.DateOfRejection = null;
                request.NextRequest = null;
                await _friendRepository.UpdateFriendsRequestAsync(request);
            }
            else
                throw new AppException($"You cannot send a request earlier than {request.NextRequest}");
        }
    }
}
