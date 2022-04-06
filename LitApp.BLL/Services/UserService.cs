using AutoMapper;
using LitApp.BLL.ModelsDto;
using LitApp.BLL.Services.Interfaces;
using LitApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LitApp.BLL.Services
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

        public async Task<List<AccountResponseDto>> GetAllUsersWithoutCurrentUserAsync(int currentUserId)
        {
            var users = await _accountRepository.GetAllAccounts().Where(x => x.Id != currentUserId).ToListAsync();
            var userDto = _mapper.Map<List<AccountResponseDto>>(users);
            return userDto;
        }
        public async Task<AccountResponseDto> GetUserByIdAsync(int id)
        {
            var user = await _accountRepository.GetAccountByIdAsync(id);
            var userDto = _mapper.Map<AccountResponseDto>(user);
            return userDto;
        }
    }
}
