﻿using AutoMapper;
using LitChat.BLL.Exceptions;
using LitChat.BLL.Jwt.Interfaces;
using LitChat.BLL.ModelsDto;
using LitChat.BLL.Services.Interfaces;
using LitChat.DAL.Models;
using LitChat.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LitChat.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IFavoritesRepository _favoriteRepository;
        private readonly IEmailService _emailService;
        private readonly IChatRepository _chatRepository;
        private readonly IJwtService _jwtOptions;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _password;

        public AccountService(
            IAccountRepository accountRepository,
            IEmailService emailService,
            IJwtService jwtOptions,
            IMapper mapper, IPasswordHasher password,
            IFavoritesRepository favoriteRepository,
            IChatRepository chatRepository)
        {
            _accountRepository = accountRepository;
            _emailService = emailService;
            _jwtOptions = jwtOptions;
            _mapper = mapper;
            _password = password;
            _favoriteRepository = favoriteRepository;
            _chatRepository = chatRepository;
        }

        public async Task<AuthenticateResponseDto> AuthenticateAsync(AuthenticateRequestDto authRequest, string ipAddress)
        {
            var account = _accountRepository.GetAllAccounts().FirstOrDefault(x => x.Email == authRequest.Email);
            if (account is null)
                throw new AppException($"Account with this {authRequest.Email } does not exist, please register your account");

            if (account != null && account.IsVerified && !_password.Verify(authRequest.Password, account.PasswordHash))
                throw new AppException($"Account with this {authRequest.Email } does not verified or entered wrong password");

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
            try
            {
                var existAccount = _accountRepository.GetAllAccounts().Any(x => x.Email == model.Email);
                if (existAccount)
                    throw new AppException($"Email '{model.Email}' is already registered, please checked your email");
                var account = _mapper.Map<Account>(model);
                account.IsVerified = false;
                account.Role = Role.Admin;
                account.Created = DateTime.Now;
                account.VerificationToken = _jwtOptions.RandomTokenString();
                account.PasswordHash = _password.HashPassword(model.Password);
                await _accountRepository.CreateAccountAsync(account);
                var accountDto = _mapper.Map<AccountDto>(account);
                await _emailService.SendVerificationEmailAsync(accountDto, origin);
            }
            catch (Exception e)
            {
                new InternalServerException(e.Message);
                throw;
            }
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
            try
            {
                var account = _accountRepository.GetAllAccounts().SingleOrDefault(x => x.Email == model.Email);
                if (account == null) return;
                account.ResetToken = _jwtOptions.RandomTokenString();
                account.ResetTokenExpires = DateTime.UtcNow.AddDays(1);
                await _accountRepository.UpdateAccountAsync(account);
                var accountDto = _mapper.Map<AccountDto>(account);
                await _emailService.SendPasswordResetEmailAsync(accountDto, origin);
            }
            catch (Exception e)
            {
                new InternalServerException(e.Message);
                throw;
            }
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
        public async Task<List<AccountResponseDto>> GetAllAccountsAsync()
        {
            var account = await _accountRepository.GetAllAccounts().ToListAsync();
            return _mapper.Map<List<AccountResponseDto>>(account);
        }

        public async Task<AccountResponseDto> GetAccountByIdAsync(int accountId)
        {
            var getAccount = await _accountRepository.GetAccountByIdAsync(accountId);
            if (getAccount == null) return null;

            return _mapper.Map<AccountResponseDto>(getAccount);
        }
        public async Task<AccountResponseDto> UpdateAccountAsync(int id, UpdateAccountDto model)
        {
            var getAccount = await _accountRepository.GetAccountAsync(id);
            if (getAccount == null)
                throw new AppException($"Account with {id} id does not found");

            getAccount.Updated = DateTime.UtcNow;
            await _accountRepository.UpdateAccountAsync(getAccount);

            return _mapper.Map<AccountResponseDto>(getAccount);
        }

        public async Task DeleteAccountAsync(int id)
        {
            var getAccount = await _accountRepository.GetAccountAsync(id);
            if (getAccount == null)
                throw new AppException($"Account with {id} id does not found");

            await _chatRepository.RemoveAllMyMessages(id);
            await _favoriteRepository.RemoveMeFromFavorite(id);
            await _accountRepository.DeleteAsync(id);
        }

        public async Task<AccountResponseDto> GetAccountByEmailAsync(string accountEmail)
        {
            var account = await _accountRepository.GetAllAccounts().FirstOrDefaultAsync(x => x.Email == accountEmail);
            if (account == null)
                throw new AppException($"User with {accountEmail} does not found");

            return _mapper.Map<AccountResponseDto>(account);
        }
    }
}