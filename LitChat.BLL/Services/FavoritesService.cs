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
            var userExist = await _favoritesRepository.GetAllFavoriteUser().AnyAsync(x=>x.FavoriteUserAccountId == user.FavoriteUserAccountId);
            if (!userExist)
                await _favoritesRepository.AddUserToFavorite(model);
        }
        public async Task<List<FavoritesListResponseDto>> GetAllFavoriteUserAsync()
        {
            var users = await _favoritesRepository.GetAllFavoriteUser().ToListAsync();
            return _mapper.Map<List<FavoritesListResponseDto>>(users);
        }
        public async Task<List<FavoritesListResponseDto>> GetAllFavoriteUserForAccountAsync(int id)
        {
            var users = await _favoritesRepository.GetAllFavoriteForAccountUser(id);
           return _mapper.Map<List<FavoritesListResponseDto>>(users);
        }

        public async Task<FavoritesListResponseDto> GetFavoriteUserByAccountIdAsync(int accountId)
        {
            var users = await _favoritesRepository.GetFavoriteUserById(accountId);
            return _mapper.Map<FavoritesListResponseDto>(users);
        }

        public async Task RemoveUserFromFavoriteAsync(int accountId)
        {
            await _favoritesRepository.RemoveUserFromFavorite(accountId);
        }
    }
}
