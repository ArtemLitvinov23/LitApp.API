using AutoMapper;
using LitBlog.BLL.Helpers;
using LitBlog.BLL.Jwt;
using LitBlog.BLL.ModelsDto;
using LitBlog.BLL.PasswordHasher;
using LitBlog.BLL.Services.Interfaces;
using LitBlog.DAL.Models;
using LitBlog.DAL.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LitBlog.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IFriendsRepository _friendsRepository;
        private readonly IEmailService _emailService;
        private readonly IJwtOptions _jwtOptions;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _password;

        public AccountService(
            IAccountRepository accountRepository, 
            IEmailService emailService,
            IJwtOptions jwtOptions,
            IMapper mapper, IPasswordHasher password,
            IFriendsRepository friendsRepository)
        {
            _accountRepository = accountRepository;
            _friendsRepository = friendsRepository;
            _emailService = emailService;
            _jwtOptions = jwtOptions;
            _mapper = mapper;
            _password = password;
        }


        public async Task<AuthenticateResponseDto> AuthenticateAsync(AuthenticateRequestDto authRequest, string ipAddress)
        { 
            var account = _accountRepository.GetAllAccounts().FirstOrDefault(x => x.Email == authRequest.Email);
            if (account != null && account.IsVerified)
                _password.Verify( authRequest.Password,account.PasswordHash);
            var accountDto = _mapper.Map<AccountDto>(account);
            var jwtToken = _jwtOptions.GenerateJwtToken(accountDto);
            var refreshToken = _jwtOptions.GenerateRefreshToken(ipAddress);
            if (account != null)
            {
                account.RefreshTokens.Add(refreshToken);

                //remove old refresh tokens from account
                _jwtOptions.RemoveOldRefreshTokens(accountDto);

                await _accountRepository.UpdateAccountAsync(account);
            }

            var response = _mapper.Map<AuthenticateResponseDto>(accountDto);
            response.JwtToken = jwtToken;
            response.RefreshToken = refreshToken.Token;
            return response;
        }


        public async Task<AuthenticateResponseDto> RefreshTokenAsync(string token, string ipAddress)
        {
            var (refreshToken, account) = _jwtOptions.GetRefreshToken(token);
            var newRefreshToken = _jwtOptions.GenerateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            account.RefreshTokens.Add(newRefreshToken);

            var accountDto = _mapper.Map<AccountDto>(account);
            _jwtOptions.RemoveOldRefreshTokens(accountDto);
            await _accountRepository.UpdateAccountAsync(account);

            var jwtToken = _jwtOptions.GenerateJwtToken(accountDto);
            var response = _mapper.Map<AuthenticateResponseDto>(accountDto);
            response.JwtToken = jwtToken;
            response.RefreshToken = newRefreshToken.Token;

            return response;
        }

        public async Task RevokeTokenAsync(string token, string ipAddress)
        {
            var (refreshToken, account) = _jwtOptions.GetRefreshToken(token);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            await _accountRepository.UpdateAccountAsync(account);
        }

        public async Task RegisterAsync(AccountDto model, string origin)
        {
            var account = _mapper.Map<Account>(model);
            account.IsVerified = false;
            account.Role = Role.Admin;
            account.Created = DateTime.Now;
            account.VerificationToken = _jwtOptions.RandomTokenString();

            account.PasswordHash = _password.HashPassword(model.Password);

            await _accountRepository.CreateAccountAsync(account);
            
             var result = _mapper.Map<AccountDto>(account);
             await _emailService.SendVerificationEmailAsync(result,origin);

        }

        public async Task VerifyEmailAsync(string token)
        {
            var account = _accountRepository.GetAllAccounts().SingleOrDefault(x => x.VerificationToken == token);
            if (account == null) throw new AppException("Verification failed");

            account.Verified = DateTime.UtcNow;
            account.IsVerified = true;
            account.VerificationToken = null;

            await _accountRepository.UpdateAccountAsync(account);

        }

        public async Task ForgotPasswordAsync(ForgotPasswordRequestDto model, string origin)
        {
            var account = _accountRepository.GetAllAccounts().SingleOrDefault(x => x.Email == model.Email);
            if (account == null) return;

            account.ResetToken = _jwtOptions.RandomTokenString();
            account.ResetTokenExpires = DateTime.UtcNow.AddDays(1);

            await _accountRepository.UpdateAccountAsync(account);

            var accountDto = _mapper.Map<AccountDto>(account);
            await _emailService.SendPasswordResetEmailAsync(accountDto,origin);
        }
        
        public async Task ValidateResetTokenAsync(RevokeTokenRequestDto model)
        {
            var account = await _accountRepository.GetAllAccounts().SingleOrDefaultAsync(x =>
                x.ResetToken == model.Token && x.ResetTokenExpires > DateTime.UtcNow);
            if (account == null)
                throw new AppException("Invalid token");
        }

        public async Task ResetPasswordAsync(ResetPasswordRequestDto model)
        {
            var account = _accountRepository.GetAllAccounts().SingleOrDefault(x =>
                x.ResetToken == model.Token && x.ResetTokenExpires > DateTime.UtcNow);
            if (account == null)
                throw new AppException("Invalid token");

            account.PasswordHash = _password.HashPassword(model.Password);
            account.PasswordReset = DateTime.UtcNow;
            account.ResetToken = null;
            account.ResetTokenExpires = null;

            await _accountRepository.UpdateAccountAsync(account);
        }

        public async Task<List<UsersResponseDto>> GetUsersAsync()
        {
            var accounts = await _accountRepository.GetAllAccounts().ToListAsync();
            return _mapper.Map<List<UsersResponseDto>>(accounts);
        }

        public async Task<List<AccountResponseDto>> GetAllAsync()
        {
            var account = await _accountRepository.GetAllAccounts().ToListAsync();
            return _mapper.Map<List<AccountResponseDto>>(account);
        }

        public async Task<AccountResponseDto> GetAccountByIdAsync(int accountId)
        {
            var account =  await _accountRepository.GetAccountByIdAsync(accountId);
            return _mapper.Map<AccountResponseDto>(account);
        }

        public async Task<AccountResponseDto> CreateAsync(AccountDto model)
        {
            if (_accountRepository.GetAllAccounts().Any(x => x.Email == model.Email))
                throw new AppException($"Email '{model.Email}' is already registered, please checked your email");

            // map model to new account object
            var account = _mapper.Map<Account>(model);
            account.Created = DateTime.UtcNow;
            account.Verified = DateTime.UtcNow;

            account.PasswordHash = _password.HashPassword(model.Password);

            await _accountRepository.CreateAccountAsync(account);

            return _mapper.Map<AccountResponseDto>(account);
        }

        public async Task<AccountResponseDto> UpdateAsync(int id, UpdateAccountDto model)
        {
            var getAccount = await _accountRepository.GetAccountAsync(id);

            if (getAccount == null)
                throw new AppException();

            // copy model to account and save
            _mapper.Map(model, getAccount);
            getAccount.Updated = DateTime.UtcNow;
            await _accountRepository.UpdateAccountAsync(getAccount);

            return _mapper.Map<AccountResponseDto>(getAccount);
        }

        public async Task DeleteAsync(int id)
        {
            await _accountRepository.DeleteAsync(id);

        }

        public bool ExistsAccount(AccountDto model)
        {
             return _accountRepository.GetAllAccounts().Any(x => x.Email == model.Email);
        } 
    }
}
