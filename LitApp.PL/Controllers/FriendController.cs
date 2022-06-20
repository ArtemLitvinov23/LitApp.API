using AutoMapper;
using LitApp.BLL.ModelsDto;
using LitApp.BLL.Services.Interfaces;
using LitApp.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitApp.PL.Controllers
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

            if (response == null)
                return BadRequest();

            return Ok(response);
        }

        [HttpGet("[action]/{accountId}")]
        public async Task<ActionResult<List<FriendViewModel>>> RejectedRequests(int accountId)
        {
            var friendsList = await _friendService.GetAllRejectedRequestsAsync(accountId);
            var response = _mapper.Map<List<FriendViewModel>>(friendsList);

            if (response == null)
                return BadRequest();

            return Ok(response);
        }

        [HttpGet("[action]/{accountId}")]
        public async Task<ActionResult<List<FriendViewModel>>> PendingRequests(int accountId)
        {
            var friendsList = await _friendService.GetAllPendingRequestsAsync(accountId);
            var response = _mapper.Map<List<FriendViewModel>>(friendsList);

            if (response == null)
            {
                return BadRequest();
            }

            return Ok(response);
        }

        [HttpGet("[action]/{friendId}")]
        public async Task<ActionResult<FriendViewModel>> GetFriend(int friendId)
        {
            var friend = await _friendService.GetFriendById(friendId);

            if (friend == null)
            {
                return BadRequest();
            }

            var response = _mapper.Map<FriendViewModel>(friend);

            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Approved(FriendRequestViewModel approvedFriend)
        {
            var maprFriendRequest = _mapper.Map<FriendRequestDto>(approvedFriend);
            await _friendService.ApprovedUserAsync(maprFriendRequest);

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Rejected(FriendRequestViewModel rejectedFriend)
        {
            var maprFriendRequest = _mapper.Map<FriendRequestDto>(rejectedFriend);
            await _friendService.RejectUserAsync(maprFriendRequest);

            return Ok();
        }

        [HttpPost("Request")]
        public async Task<IActionResult> CreateRequestToFriend(FriendRequestViewModel request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            var maprFriendRequest = _mapper.Map<FriendRequestDto>(request);
            await _friendService.SendRequestToUserAsync(maprFriendRequest);
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
