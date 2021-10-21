using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using LitBlog.BLL.ModelsDto;
using LitBlog.BLL.Settings;
using LitBlog.DAL.Models;
using LitBlog.DAL.Repositories;
using Microsoft.Extensions.Options;

namespace LitBlog.BLL.Jwt
{
    public class JwtService:IJwtOptions
    {
        private readonly AppSettings _appSettings;
        private readonly IAccountRepository _accountRepository;

        public JwtService(IOptions<AppSettings> appSettings, IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _appSettings = appSettings.Value;
        }
        public string GenerateJwtToken(AccountCreateDto account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key =  new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {new Claim("id", account.Id.ToString())}),
                Expires = DateTime.UtcNow.AddMinutes(_appSettings.TokenLifeTime),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public (RefreshToken, Account) GetRefreshToken(string token)
        {
            var account = _accountRepository.GetRefreshToken(token);
            if (account == null)
                throw new ApplicationException("InvalidToken");
            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);
            if (!refreshToken.IsActive)
                throw new ApplicationException("Invalid token");
            return (refreshToken, account);
        }

        public RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }

        public void RemoveOldRefreshTokens(AccountCreateDto account)
        {
            account.RefreshTokens.RemoveAll(x =>
                !x.IsActive && x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }

        public string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return BitConverter.ToString(randomBytes).Replace(" - ", " ");
        }
        
    }
}
