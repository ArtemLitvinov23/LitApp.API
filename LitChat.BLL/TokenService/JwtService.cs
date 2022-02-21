﻿using LitChat.BLL.Exceptions;
using LitChat.BLL.Jwt.Interfaces;
using LitChat.BLL.Jwt.Options;
using LitChat.BLL.ModelsDto;
using LitChat.DAL.Models;
using LitChat.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LitChat.BLL.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly TokenOptions _appSettings;
        private readonly IAccountRepository _accountRepository;

        public JwtService(IOptions<TokenOptions> appSettings, IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _appSettings = appSettings.Value;
        }
        public string GenerateJwtToken(AccountDto account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id",account.Id.ToString()),
                                                     new Claim(ClaimTypes.Email,account.Email)}),
                Expires = DateTime.Now.AddDays(double.Parse(_appSettings.TokenLifeTime)),
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
                !x.IsActive && x.Created.AddDays(double.Parse(_appSettings.RefreshTokenTTL)) <= DateTime.UtcNow);
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