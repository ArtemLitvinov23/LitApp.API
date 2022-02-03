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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoritesService _favoritesService;
        private readonly IMapper _mapper;

        public FavoritesController(
            IFavoritesService favoritesService,
            IMapper mapper)
        {
            _favoritesService = favoritesService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<FavoritesListResponseViewModel>>> GetAllFavoritesAsync()
        {
            var users = await _favoritesService.GetAllFavoriteUserAsync();
            var result = _mapper.Map<List<FavoritesListResponseViewModel>>(users);
            return Ok(result);
        }
        [HttpGet("get-users/{id}")]
        public async Task<ActionResult<List<FavoritesListResponseViewModel>>> GetAllFavoritesForAccountAsync(int id)
        {
            var users = await _favoritesService.GetAllFavoriteUserForAccountAsync(id);
            var result = _mapper.Map<List<FavoritesListResponseViewModel>>(users);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<FavoritesListResponseViewModel>> GetFavoriteUserAsync(int id)
        {
            var users = await _favoritesService.GetFavoriteUserByAccountIdAsync(id);
            var result = _mapper.Map<FavoritesListResponseViewModel>(users);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToFavoritesAsync(FavoritesListViewModel favoritesListViewModel)
        {
            var users = _mapper.Map<FavoritesListDto>(favoritesListViewModel);
            await _favoritesService.AddUserToFavoriteAsync(users);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveUserFavoritesAsync(int id)
        {
            await _favoritesService.RemoveUserFromFavoriteAsync(id);
            return Ok();
        }
    }
}
