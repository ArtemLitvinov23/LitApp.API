using AutoMapper;
using LitChat.BLL.Exceptions;
using LitChat.BLL.ModelsDto;
using LitChat.BLL.Services.Interfaces;
using LitChat.DAL.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace LitChat.BLL.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public UserProfileService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task AddInfoAboutAccountAsync(int id, UserInfoDto addUserInfoRequest)
        {
            var getAccount = await _accountRepository.GetAccountByIdAsync(id);
            if (getAccount == null)
                throw new AppException();
            _mapper.Map(addUserInfoRequest, getAccount);
            getAccount.Updated = DateTime.UtcNow;
            await _accountRepository.UpdateAccountAsync(getAccount);
        }

        public async Task<UserInfoDto> GetUserInfoAsync(int id)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);
            return _mapper.Map<UserInfoDto>(account);
        }

        public async Task RemovePhoneFromAccountAsync(int id)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);
            account.Profile.Phone = null;
            await _accountRepository.UpdateAccountAsync(account);
        }
        public async Task RemoveDescriptionFromAccountAsync(int id)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);
            account.Profile.Description = null;
            await _accountRepository.UpdateAccountAsync(account);
        }
    }
}
