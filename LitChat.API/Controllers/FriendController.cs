using AutoMapper;
using LitChat.API.Models;
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

        public FriendController(
            IMapper mapper,
            IFriendService friendService)
        {
            _mapper = mapper;
            _friendService = friendService;
        }

        [HttpGet("[action]/{accountId}")]
        public async Task<ActionResult<List<FriendViewModel>>> FriendsList(int accountId)
        {
            var friendsList = await _friendService.GetAllApprovedFriendsAsync(accountId);
            var response = _mapper.Map<List<FriendViewModel>>(friendsList);
            return Ok(response);
        }

        [HttpGet("[action]/{accountId}")]
        public async Task<ActionResult<List<FriendViewModel>>> RejectedRequests(int accountId)
        {
            var friendsList = await _friendService.GetAllRejectedRequestsAsync(accountId);
            var response = _mapper.Map<List<FriendViewModel>>(friendsList);
            return Ok(response);
        }

        [HttpGet("[action]/{accountId}")]
        public async Task<ActionResult<List<FriendViewModel>>> PendingRequests(int accountId)
        {
            var friendsList = await _friendService.GetAllPendingRequestsAsync(accountId);
            var response = _mapper.Map<List<FriendViewModel>>(friendsList);
            return Ok(response);
        }

        [HttpGet("[action]/{friendId}")]
        public async Task<ActionResult<FriendViewModel>> GetFriend(int friendId)
        {
            var friend = await _friendService.GetFriendById(friendId);
            if (friend == null) return BadRequest();

            var response = _mapper.Map<FriendViewModel>(friend);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Approved(FriendRequestViewModel approvedFriend)
        {

            await _friendService.ApprovedUserAsync(approvedFriend.AccountId, approvedFriend.FriendAccountId);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Rejected(FriendRequestViewModel rejectedFriend)
        {
            await _friendService.RejectUserAsync(rejectedFriend.AccountId, rejectedFriend.FriendAccountId);
            return Ok();
        }

        [HttpPost("Request")]
        public async Task<IActionResult> CreateRequestToFriend(FriendRequestViewModel request)
        {
            if (request == null) return BadRequest();

            await _friendService.SendRequestToUserAsync(request.AccountId, request.FriendAccountId);
            return Ok();
        }

        [HttpDelete("{friendId}")]
        public async Task<IActionResult> DeleteFriend(int friendId)
        {

            await _friendService.DeleteUserFromFriends(friendId);
            return Ok();
        }
    }
}
