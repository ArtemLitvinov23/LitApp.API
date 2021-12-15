using AutoMapper;
using LitChat.BLL.ModelsDto;
using LitChat.BLL.Services.Interfaces;
using LitChat.DAL.Models;
using LitChat.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.BLL.Services
{
    public class FavoritesService : IFavoritesService
    {
        private readonly IFavoritesRepository _favoritesRepository;
        private readonly IMapper _mapper;

        public FavoritesService(IFavoritesRepository favoritesRepository,
            IMapper mapper)
        {
            _favoritesRepository = favoritesRepository;
            _mapper = mapper;
        }
        public async Task AddUserToFavoriteAsync(FavoritesListDto user)
        {
            var model = _mapper.Map<FavoritesList>(user);
            await _favoritesRepository.AddUserToFavorite(model);
        }

        public async Task<List<FavoritesListResponseDto>> GetAllFavoriteUserAsync()
        {
           var users = await _favoritesRepository.GetAllFavoriteUser().ToListAsync();
           return _mapper.Map<List<FavoritesListResponseDto>>(users);
        }

        public async Task<FavoritesListResponseDto> GetFavoriteUserByIdAsync(int id)
        {
            var users = await _favoritesRepository.GetFavoriteUserById(id);
            return _mapper.Map<FavoritesListResponseDto>(users);
        }

        public async Task RemoveUserFromFavoriteAsync(int id)
        {
            await _favoritesRepository.RemoveUserFromFavorite(id);
        }
    }
}
