using AutoMapper;
using LitChat.BLL.Exceptions;
using LitChat.BLL.ModelsDto;
using LitChat.BLL.Services.Interfaces;
using LitChat.DAL.Models;
using LitChat.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LitChat.BLL.Services
{
    public class FriendService : IFriendService
    {

        private readonly IFriendRepository _friendRepository;
        private readonly IMapper _mapper;
        public FriendService(
            IFriendRepository friendRepository,
            IMapper mapper)
        {
            _friendRepository = friendRepository;
            _mapper = mapper;
        }

        public async Task ApprovedUserAsync(AccountDto sender, AccountDto friend)
        {
            var senderAccount = _mapper.Map<Account>(sender);
            var friendAccount = _mapper.Map<Account>(friend);
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

        public async Task DeleteUserFromFriends(AccountDto friendAccount)
        {
            if (friendAccount == null) throw new AppException("Account is not found");

            await _friendRepository.RemoveUserFromFriends(friendAccount.Id);
        }

        public async Task<List<FriendDto>> GetAllApprovedFriendsAsync()
        {
            var friends = await _friendRepository.GetAllFriends().Where(x => x.RequestFlags == RequestFlags.Approved).ToListAsync();
            return _mapper.Map<List<FriendDto>>(friends);
        }
           

        public async Task<List<FriendDto>> GetAllPendingRequestsAsync() => _mapper.Map<List<FriendDto>>(await _friendRepository.GetAllFriends().Where(x => x.RequestFlags == RequestFlags.Pending).ToListAsync());

        public async Task<List<FriendDto>> GetAllRejectedRequestsAsync() => _mapper.Map<List<FriendDto>>(await _friendRepository.GetAllFriends().Where(x => x.RequestFlags == RequestFlags.Rejected).ToListAsync());

        public async Task<UsersResponseDto> GetFriendById(int friendAccountId)
        {
            var friend = await _friendRepository.GetFriendById(friendAccountId);
            if (friend == null) return null;

            var response = _mapper.Map<UsersResponseDto>(friend);
            return response;
        }

        public async Task RejectUserAsync(AccountDto account, AccountDto friend)
        {
            var myAccount = _mapper.Map<Account>(account);
            var friendAccount = _mapper.Map<Account>(friend);

            var response = await _friendRepository.GetRequests(myAccount, friendAccount);
            if (response == null) throw new AppException("Current request is not found");

            response.RequestTime = null;
            response.RequestFlags = RequestFlags.Rejected;
            response.DateOfRejection = DateTime.Now.ToLocalTime();
            response.NextRequest = DateTime.Now.AddDays(2).ToLocalTime();
            await _friendRepository.UpdateFriendsRequestAsync(response);
        }

        public async Task SendRequestToUserAsync(AccountDto sender, AccountDto friend)
        {
            var senderAccount = _mapper.Map<Account>(sender);
            var friendAccount = _mapper.Map<Account>(friend);
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
