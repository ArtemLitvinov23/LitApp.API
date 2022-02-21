using AutoMapper;
using LitChat.API.Models;
using LitChat.BLL.ModelsDto;
using LitChat.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FriendController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFriendService _friendService;
        private readonly IAccountService _accountService;

        public FriendController(
            IMapper mapper,
            IFriendService friendService,
            IAccountService accountService)
        {
            _mapper = mapper;
            _friendService = friendService;
            _accountService = accountService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<FriendViewModel>>> FriendsList()
        {
            var friendsList = await _friendService.GetAllApprovedFriendsAsync();
            var response = _mapper.Map<List<FriendViewModel>>(friendsList);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<FriendViewModel>>> RejectedRequests()
        {
            var friendsList = await _friendService.GetAllRejectedRequestsAsync();
            var response = _mapper.Map<List<FriendViewModel>>(friendsList);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<FriendViewModel>>> PendingRequests()
        {
            var friendsList = await _friendService.GetAllPendingRequestsAsync();
            var response = _mapper.Map<List<FriendViewModel>>(friendsList);
            return Ok(response);
        }

        [HttpGet("[action]/{friendId}")]
        public async Task<ActionResult<UserResponseViewModel>> GetFriend(int friendId)
        {
            var friend = await _friendService.GetFriendById(friendId);
            if (friend == null) return BadRequest();

            var response = _mapper.Map<UserResponseViewModel>(friend);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Approved(FriendRequestViewModel approvedFriendViewModel)
        {
            var senderAccount = await _accountService.GetAccountByIdAsync(approvedFriendViewModel.AccountId);
            var friendAccount = await _accountService.GetAccountByIdAsync(approvedFriendViewModel.FriendAccountId);
            if (senderAccount == null || friendAccount == null) return BadRequest();

            var sender = _mapper.Map<AccountDto>(senderAccount);
            var friend = _mapper.Map<AccountDto>(friendAccount);

            await _friendService.ApprovedUserAsync(sender, friend);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Rejected(FriendRequestViewModel approvedFriendViewModel)
        {
            var senderAccount = await _accountService.GetAccountByIdAsync(approvedFriendViewModel.AccountId);
            var friendAccount = await _accountService.GetAccountByIdAsync(approvedFriendViewModel.FriendAccountId);
            if (senderAccount == null || friendAccount == null) return BadRequest();

            var sender = _mapper.Map<AccountDto>(senderAccount);
            var friend = _mapper.Map<AccountDto>(friendAccount);

            await _friendService.RejectUserAsync(sender, friend);
            return Ok();
        }

        [HttpPost("Request")]
        public async Task<IActionResult> CreateRequestToFriend(FriendRequestViewModel request)
        {
            var senderAccount = await _accountService.GetAccountByIdAsync(request.AccountId);
            var friendAccount = await _accountService.GetAccountByIdAsync(request.FriendAccountId);
            if (senderAccount == null || friendAccount == null) return BadRequest();

            var sender = _mapper.Map<AccountDto>(senderAccount);
            var friend = _mapper.Map<AccountDto>(friendAccount);

            await _friendService.SendRequestToUserAsync(sender, friend);
            return Ok();
        }

        [HttpDelete("{friendId}")]
        public async Task<IActionResult> DeleteFriend(int friendId)
        {
            var getFriendAccount = await _accountService.GetAccountByIdAsync(friendId);
            if (getFriendAccount == null) return BadRequest();

            var friendAccount = _mapper.Map<AccountDto>(getFriendAccount);
            await _friendService.DeleteUserFromFriends(friendAccount);
            return Ok();
        }
    }
}
