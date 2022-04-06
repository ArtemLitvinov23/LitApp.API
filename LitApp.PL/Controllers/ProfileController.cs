using AutoMapper;
using LitApp.BLL.ModelsDto;
using LitApp.BLL.Services.Interfaces;
using LitApp.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LitChat.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IUserProfileService _profileService;
        private readonly IMapper _mapper;

        public ProfileController(
            IUserProfileService profileService,
            IMapper mapper)
        {
            _profileService = profileService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserInfoViewModel>> GetUserInfoAsync(string id)
        {
            var users = await _profileService.GetUserInfoAsync(int.Parse(id));
            var result = _mapper.Map<UserInfoViewModel>(users);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> AddUserInfoAsync(string id, UserInfoViewModel model)
        {
            var result = _mapper.Map<UserInfoDto>(model);
            await _profileService.AddInfoAboutAccountAsync(int.Parse(id), result);
            return Ok();
        }

        [HttpPatch("phone/{id}")]
        public async Task<IActionResult> RemovePhoneAsync(string id)
        {
            await _profileService.RemovePhoneFromAccountAsync(int.Parse(id));
            return Ok();
        }

        [HttpPatch("description/{id}")]
        public async Task<IActionResult> RemoveDescriptionAsync(string id)
        {
            await _profileService.RemoveDescriptionFromAccountAsync(int.Parse(id));
            return Ok();
        }
    }
}
