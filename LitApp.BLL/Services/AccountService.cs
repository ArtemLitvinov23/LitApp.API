using AutoMapper;
using LitApp.BLL.Exceptions;
using LitApp.BLL.ModelsDto;
using LitApp.BLL.Services.Interfaces;
using LitApp.DAL.Models;
using LitApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LitApp.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IFavoritesRepository _favoriteRepository;
        private readonly IEmailService _emailService;
        private readonly IChatRepository _chatRepository;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _password;
        private IConfiguration Configuration { get; }

        public AccountService(
            IAccountRepository accountRepository,
            IEmailService emailService,
            IJwtService jwtService,
            IMapper mapper,
            IPasswordHasher password,
            IFavoritesRepository favoriteRepository,
            IChatRepository chatRepository,
            IConfiguration configuration)
        {
            _accountRepository = accountRepository;
            _emailService = emailService;
            _jwtService = jwtService;
            _mapper = mapper;
            _password = password;
            _favoriteRepository = favoriteRepository;
            _chatRepository = chatRepository;
            Configuration = configuration;
        }

        public async Task<AuthenticateResponseDto> AuthenticateAsync(AuthenticateRequestDto authRequest, string ipAddress)
        {
            var account = _accountRepository.GetAllAccounts().FirstOrDefault(x => x.Email == authRequest.Email);

            if (account is null)
                throw new AppException($"Account with this {authRequest.Email} does not exist, please register your account");

            if (!account.IsVerified || !_password.Verify(authRequest.Password, account.PasswordHash))
                throw new AppException($"Account with this {authRequest.Email } does not verified or entered wrong password");

            var accountDto = _mapper.Map<AccountDto>(account);

            var jwtToken = _jwtService.GenerateJwtToken(accountDto);

            var refreshToken = _jwtService.GenerateRefreshToken(ipAddress);

            if (account is not null)
            {
                account.RefreshTokens.Add(refreshToken);
                //remove old refresh tokens from account
                _jwtService.RemoveOldRefreshTokens(accountDto);
                account.TokenExpires = DateTime.Now.AddDays(double.Parse(Configuration["JwtConfig:TokenLifeTime"]));
                await _accountRepository.UpdateAccountAsync(account);
            }

            var response = _mapper.Map<AuthenticateResponseDto>(accountDto);

            response.JwtToken = jwtToken;
            response.RefreshToken = refreshToken.Token;

            return response;
        }


        public async Task<AuthenticateResponseDto> RefreshTokenAsync(string token, string ipAddress)
        {
            var (refreshToken, account) = await _jwtService.GetRefreshToken(token);

            var newRefreshToken = _jwtService.GenerateRefreshToken(ipAddress);

            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            account.RefreshTokens.Add(newRefreshToken);

            var accountDto = _mapper.Map<AccountDto>(account);

            _jwtService.RemoveOldRefreshTokens(accountDto);

            await _accountRepository.UpdateAccountAsync(account);

            var jwtToken = _jwtService.GenerateJwtToken(accountDto);

            var response = _mapper.Map<AuthenticateResponseDto>(accountDto);

            response.JwtToken = jwtToken;
            response.RefreshToken = newRefreshToken.Token;

            return response;
        }

        public async Task RevokeTokenAsync(string token, string ipAddress)
        {
            var (refreshToken, account) = await _jwtService.GetRefreshToken(token);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;

            await _accountRepository.UpdateAccountAsync(account);
        }

        public async Task RegisterAsync(AccountDto model, string origin)
        {
            var existAccount = await _accountRepository.GetAllAccounts().AnyAsync(x => x.Email == model.Email);

            if (existAccount)
                throw new AppException($"Email '{model.Email}' is already registered, please checked your email");

            var account = _mapper.Map<Account>(model);

            account.VerificationToken = _jwtService.RandomTokenString();
            account.PasswordHash = _password.HashPassword(model.Password);
            await _accountRepository.CreateAccountAsync(account);

            await _emailService.SendVerificationEmailAsync(account, origin);
        }

        public async Task VerifyEmailAsync(string token)
        {
            var account = await _accountRepository.GetAllAccounts().SingleOrDefaultAsync(x => x.VerificationToken == token);

            if (account is null)
            {
                throw new AppException("Verification failed");
            }

            account.Verified = DateTime.UtcNow;
            account.IsVerified = true;
            account.VerificationToken = null;

            await _accountRepository.UpdateAccountAsync(account);

        }

        public async Task ForgotPasswordAsync(ForgotPasswordRequestDto model, string origin)
        {
            var account = await _accountRepository.GetAllAccounts().SingleOrDefaultAsync(x => x.Email == model.Email);

            if (account is null)
            {
                return;
            }

            account.ResetToken = _jwtService.RandomTokenString();
            account.ResetTokenExpires = DateTime.UtcNow.AddDays(1);
            await _accountRepository.UpdateAccountAsync(account);

            await _emailService.SendPasswordResetEmailAsync(account, origin);
        }

        public async Task ValidateResetTokenAsync(RevokeTokenRequestDto model)
        {
            var account = await _accountRepository.GetAllAccounts().SingleOrDefaultAsync(x =>
                x.ResetToken == model.Token && x.ResetTokenExpires > DateTime.UtcNow);

        }

        public async Task ResetPasswordAsync(ResetPasswordRequestDto model)
        {
            var account = _accountRepository.GetAllAccounts().SingleOrDefault(x =>
                x.ResetToken == model.Token && x.ResetTokenExpires > DateTime.UtcNow);

            account.PasswordHash = _password.HashPassword(model.Password);
            account.PasswordReset = DateTime.UtcNow;
            account.ResetToken = null;
            account.ResetTokenExpires = null;

            await _accountRepository.UpdateAccountAsync(account);
        }
        public async Task<List<AccountResponseDto>> GetAllAccountsAsync()
        {
            var cacheAccount = await _accountRepository.GetAllAccounts().ToListAsync();

            return _mapper.Map<List<AccountResponseDto>>(cacheAccount);
        }

        public async Task<AccountResponseDto> GetAccountByIdAsync(int accountId)
        {
            var account = await _accountRepository.GetAccountByIdAsync(accountId);

            return _mapper.Map<AccountResponseDto>(account);
        }
        public async Task<AccountResponseDto> UpdateAccountAsync(UpdateAccountDto model)
        {
            var getAccount = await _accountRepository.GetAccountByIdAsync(model.Id);
            if (getAccount is null)
                throw new AppException($"Account with {model.Id} id does not found");

            getAccount.Profile.FirstName = model.Profile.FirstName;
            getAccount.Profile.LastName = model.Profile.LastName;
            getAccount.Profile.Description = model.Profile.Description;
            getAccount.Profile.Phone = model.Profile.Phone;
            getAccount.Updated = DateTime.UtcNow;

            await _accountRepository.UpdateAccountAsync(getAccount);

            return _mapper.Map<AccountResponseDto>(getAccount);
        }

        public async Task DeleteAccountAsync(int id)
        {
            var getAccount = await _accountRepository.GetAccountByIdAsync(id);

            if (getAccount is null)
                throw new AppException($"Account with {id} id does not found");

            await _chatRepository.RemoveAllMyMessages(id);
            await _favoriteRepository.RemoveMeFromFavorite(id);
            await _accountRepository.DeleteAsync(id);
        }

    }
}
