using AutoMapper;
using LitChat.BLL.ModelsDto;
using LitChat.BLL.Services.Interfaces;
using LitChat.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LitChat.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public UserService(
            IAccountRepository accountRepository,
            IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<List<UsersResponseDto>> GetAllUsersAsync(int currentUserId)
        {
            var users = await _accountRepository.GetAllAccounts().Where(x => x.Id != currentUserId).ToListAsync();
            var userDto = _mapper.Map<List<UsersResponseDto>>(users);
            return userDto;
        }
        public async Task<UsersResponseDto> GetUserByIdAsync(int id)
        {
            var user = await _accountRepository.GetAccountByIdAsync(id);
            var userDto = _mapper.Map<UsersResponseDto>(user);
            return userDto;
        }
    }
}
