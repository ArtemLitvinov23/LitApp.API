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

        public async Task Register(AccountCreateDto model, string origin)
        {
            if (_accountRepository.GetAllAccounts().Any(x => x.Email == model.Email))
                _emailService.SendAlreadyRegisteredEmail(model, origin);

            var account = _mapper.Map<Account>(model);

            var isFirstAccount = _accountRepository.GetAllAccounts().Any();
            account.Role = isFirstAccount ? Role.Admin : Role.User;
            account.Created = DateTime.UtcNow;
            account.VerificationToken = _jwtOptions.RandomTokenString();

            account.PasswordHash = _password.HashPassword(model.Password);

            await _accountRepository.CreateAccount(account);

            var accountDto = _mapper.Map<AccountCreateDto>(account);
            _emailService.SendVerificationEmail(accountDto,origin);

        }

        public async Task VerifyEmail(string token)
        {
            var account = _accountRepository.GetAllAccounts().SingleOrDefault(x => x.VerificationToken == token);
            if (account == null) throw new ApplicationException("Verification failed");

            account.Verified = DateTime.UtcNow;
            account.VerificationToken = null;

            await _accountRepository.UpdateAccount(account);

        }

        public async Task ForgotPassword(ForgotPasswordRequestDto model, string origin)
        {
            var account = _accountRepository.GetAllAccounts().SingleOrDefault(x => x.Email == model.Email);
            if (account == null) return;

            account.ResetToken = _jwtOptions.RandomTokenString();
            account.ResetTokenExpires = DateTime.UtcNow.AddDays(1);

            await _accountRepository.UpdateAccount(account);

            var accountDto = _mapper.Map<AccountCreateDto>(account);
            _emailService.SendPasswordResetEmail(accountDto,origin);
        }

        public void ValidateResetToken(RevokeTokenRequest model)
        {
            var account = _accountRepository.GetAllAccounts().SingleOrDefault(x =>
                x.ResetToken == model.Token && x.ResetTokenExpires > DateTime.UtcNow);
            if (account == null)
                throw new ApplicationException("Invalid token");
        }

        public async Task ResetPassword(ResetPasswordRequest model)
        {
            var account = _accountRepository.GetAllAccounts().SingleOrDefault(x =>
                x.ResetToken == model.Token && x.ResetTokenExpires > DateTime.UtcNow);
            if (account == null)
                throw new ApplicationException("Invalid token");

            account.PasswordHash = _password.HashPassword(model.Password);
            account.PasswordReset = DateTime.UtcNow;
            account.ResetToken = null;
            account.ResetTokenExpires = null;

            await _accountRepository.UpdateAccount(account);
        }

        public IQueryable<AccountResponseDto> GetAll()
        {
            var account = _accountRepository.GetAllAccounts();
            return _mapper.Map<IQueryable<AccountResponseDto>>(account);
        }

        public async Task<AccountResponseDto> GetAccountById(int accountId)
        {
            var account =  await _accountRepository.GetAccountById(accountId);
            return _mapper.Map<AccountResponseDto>(account);
        }

        public async Task<AccountResponseDto> Create(AccountCreateDto model)
        {
            if (_accountRepository.GetAllAccounts().Any(x => x.Email == model.Email))
                throw new ApplicationException($"Email '{model.Email}' is already registered");

            // map model to new account object
            var account = _mapper.Map<Account>(model);
            account.Created = DateTime.UtcNow;
            account.Verified = DateTime.UtcNow;

            account.PasswordHash = _password.HashPassword(model.Password);

            await _accountRepository.CreateAccount(account);

            return _mapper.Map<AccountResponseDto>(account);
        }

        public async Task<AccountResponseDto> Update(int id, UpdateAccount model)
        {
            var getAccount = _accountRepository.GetAccount(id);

            if (getAccount.Email != model.Email && _accountRepository.GetAllAccounts().Any(x=>x.Email == model.Email))
                throw new ApplicationException($"Email {model.Email}i s alerady");

            if (!string.IsNullOrEmpty(model.Password))
                getAccount.PasswordHash = _password.HashPassword(model.Password);

            // copy model to account and save
            _mapper.Map(model, getAccount);
            getAccount.Updated = DateTime.UtcNow;
            await _accountRepository.UpdateAccount(getAccount);

            return _mapper.Map<AccountResponseDto>(getAccount);
        }

        public void Delete(int id)
        {
            _accountRepository.Delete(id);

        }
    }
}
