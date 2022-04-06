using LitApp.BLL.Exceptions;
using LitApp.BLL.ModelsDto;
using LitApp.BLL.Services.Interfaces;
using LitApp.DAL.Models;
using LitApp.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LitApp.BLL.Services
{
    public class TokenService : IJwtService
    {
        private readonly IAccountRepository _accountRepository;
        private IConfiguration Configuration { get; }

        public TokenService(
            IAccountRepository accountRepository,
            IConfiguration configuration)
        {
            _accountRepository = accountRepository;
            Configuration = configuration;
        }
        public string GenerateJwtToken(AccountDto accountDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id",accountDto.Id.ToString()),
                                                     new Claim(ClaimTypes.Email,accountDto.Email)}),
                Expires = DateTime.Now.AddDays(double.Parse(Configuration["JwtConfig:TokenLifeTime"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

        public (RefreshToken, Account) GetRefreshToken(string token)
        {
            var account = _accountRepository.GetRefreshToken(token);
            if (account == null)
                throw new AppException("InvalidToken");
            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);
            if (!refreshToken.IsActive)
                throw new AppException("Invalid token");
            return (refreshToken, account);
        }

        public RefreshToken GenerateRefreshToken(string ipAddress)
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }

        public void RemoveOldRefreshTokens(AccountDto account)
        {
            account.RefreshTokens.RemoveAll(x =>
                !x.IsActive && x.Created.AddDays(double.Parse(Configuration["JwtConfig:RefreshTokenTTL"])) <= DateTime.UtcNow);
        }

        public string RandomTokenString()
        {
            var randomBytes = new byte[40];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return BitConverter.ToString(randomBytes).Replace(" - ", " ");
        }
    }
}
