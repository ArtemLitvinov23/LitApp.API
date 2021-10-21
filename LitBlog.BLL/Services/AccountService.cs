using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LitBlog.BLL.Jwt;
using LitBlog.BLL.ModelsDto;
using LitBlog.BLL.PasswordHasher;
using LitBlog.BLL.Services.Interfaces;
using LitBlog.DAL.Models;
using LitBlog.DAL.Repositories;

namespace LitBlog.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailService _emailService;
        private readonly IJwtOptions _jwtOptions;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _password;

        public AccountService(
            IAccountRepository accountRepository, 
            IEmailService emailService,
            IJwtOptions jwtOptions,
            IMapper mapper, IPasswordHasher password)
        {
            _accountRepository = accountRepository;
            _emailService = emailService;
            _jwtOptions = jwtOptions;
            _mapper = mapper;
            _password = password;
        }


        public async Task<AuthenticateResponseDto> Authenticate(AuthenticateRequest authRequest, string ipAddress)
        {
            var account = _accountRepository.GetAllAccounts().FirstOrDefault(x => x.Email == authRequest.Email);
            if (account == null || !account.IsVerified || !_password.Verify(authRequest.Password, account.PasswordHash))
                throw new ApplicationException("Email or password is incorrect");

            var accountDto = _mapper.Map<AccountCreateDto>(account);
            var jwtToken = _jwtOptions.GenerateJwtToken(accountDto);
            var refreshToken = _jwtOptions.GenerateRefreshToken(ipAddress);
            account.RefreshTokens.Add(refreshToken);

            //remove old refresh tokens from account
            _jwtOptions.RemoveOldRefreshTokens(accountDto);

            await _accountRepository.UpdateAccount(account);

            var response = _mapper.Map<AuthenticateResponseDto>(accountDto);
            response.JwtToken = jwtToken;
            response.RefreshToken = refreshToken.Token;
            return response;
        }

        public async Task<AuthenticateResponseDto> RefreshToken(string token, string ipAddress)
        {
            var (refreshToken, account) = _jwtOptions.GetRefreshToken(token);
            var newRefreshToken = _jwtOptions.GenerateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            account.RefreshTokens.Add(newRefreshToken);

            var accountDto = _mapper.Map<AccountCreateDto>(account);
            _jwtOptions.RemoveOldRefreshTokens(accountDto);
            await _accountRepository.UpdateAccount(account);

            var jwtToken = _jwtOptions.GenerateJwtToken(accountDto);
            var response = _mapper.Map<AuthenticateResponseDto>(accountDto);
            response.JwtToken = jwtToken;
            response.RefreshToken = newRefreshToken.Token;

            return response;
        }

        public async Task RevokeToken(string token, string ipAddress)
        {
            var (refreshToken, account) = _jwtOptions.GetRefreshToken(token);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            await _accountRepository.UpdateAccount(account);
        }

        public Task Register(AccountCreateDto model, string origin)
        {
            var account = _accountRepository.GetAllAccounts().Any(x => x.Email == model.Email);
            if (account)
            {
                
            }
        }

        public Task VerifyEmail(string token)
        {
            throw new NotImplementedException();
        }

        public Task ForgotPassword(ForgotPasswordRequestDto model, string origin)
        {
            throw new NotImplementedException();
        }

        public Task ValidateResetToken(RevokeTokenRequest model)
        {
            throw new NotImplementedException();
        }

        public Task ResetPassword(ResetPasswordRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AccountResponseDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<AccountResponseDto> GetAccountById(int accountId)
        {
            throw new NotImplementedException();
        }

        public Task<AccountResponseDto> Create(AccountCreateDto model)
        {
            throw new NotImplementedException();
        }

        public Task<AccountResponseDto> Update(int id, UpdateAccount model)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
