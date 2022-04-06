using AutoMapper;
using LitApp.BLL.Services.Interfaces;
using LitApp.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("[action]/{currentUserId}")]
        public async Task<ActionResult<List<AccountResponseViewModel>>> GetAllUsersWithoutCurrent(int currentUserId)
        {
            var result = await _userService.GetAllUsersWithoutCurrentUserAsync(currentUserId);
            return Ok(_mapper.Map<List<AccountResponseViewModel>>(result));
        }

        [Authorize]
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<AccountResponseViewModel>> GetUserById(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            return Ok(_mapper.Map<AccountResponseViewModel>(result));
        }
    }
}
