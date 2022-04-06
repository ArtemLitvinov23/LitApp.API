using LitApp.BLL.Exceptions;
using LitApp.BLL.ModelsDto;
using LitApp.BLL.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitApp.BLL.Services
{
    public class CacheService : ICacheService<AccountResponseDto>
    {
        private readonly IDistributedCache _distributedCache;
        private readonly DistributedCacheEntryOptions _options;

        private const string Prefix = "litchat_";
        private const string keyWord = "list";
        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            _options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(120),
                SlidingExpiration = TimeSpan.FromSeconds(60)
            };
        }

        public async Task<AccountResponseDto> Get(int id)
        {
            var key = Prefix + id;
            try
            {
                var cache = await _distributedCache.GetStringAsync(key);

                if (cache is null)
                    return null;

                var account = JsonConvert.DeserializeObject<AccountResponseDto>(cache);

                return account;
            }
            catch
            {
                return null;
            }
        }


        public async Task<List<AccountResponseDto>> GetList()
        {
            var key = Prefix + keyWord;
            try
            {
                var cache = await _distributedCache.GetStringAsync(key);

                if (cache is null)
                    return null;

                var account = JsonConvert.DeserializeObject<List<AccountResponseDto>>(cache);

                return account;
            }
            catch
            {
                return null;
            }
        }
        public async Task Set(AccountResponseDto content)
        {
            var key = Prefix + content.Id;
            try
            {
                var accountString = JsonConvert.SerializeObject(content);
                await _distributedCache.SetStringAsync(key, accountString,
                    _options);
            }
            catch (Exception ex)
            {
                throw new InternalServerException(ex.Message);
            }
        }
        public async Task SetList(List<AccountResponseDto> item)
        {
            var key = Prefix + keyWord;
            try
            {
                var accountString = JsonConvert.SerializeObject(item);
                await _distributedCache.SetStringAsync(key, accountString, _options);
            }
            catch (Exception ex)
            {
                throw new InternalServerException(ex.Message);
            }
        }
    }
}
