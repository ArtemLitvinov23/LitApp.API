using LitChat.Blazor.Models;
using LitChat.Blazor.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LitChat.Blazor.Services
{
    public class HttpServiceGeneric : IHttpServiceGeneric
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;


        public HttpServiceGeneric(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task Delete(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            var savedToken = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken.JwtToken);
            await _httpClient.SendAsync(request);
        }
        public async Task<T> Get<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var savedToken = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken.JwtToken);
            var httpResponse = await _httpClient.SendAsync(request);
            return await httpResponse.Content.ReadFromJsonAsync<T>();
        }

        public async Task<List<T>> GetAll<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var savedToken = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken.JwtToken);
            var httpResponse = await _httpClient.SendAsync(request);
            return await httpResponse.Content.ReadFromJsonAsync<List<T>>();
        }

        public async Task Patch(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            var savedToken = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken.JwtToken);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            await _httpClient.SendAsync(request);
        }

        public async Task Patch(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, uri);
            var savedToken = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken.JwtToken);
            await _httpClient.SendAsync(request);
        }

        public async Task Post(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            var savedToken = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken.JwtToken);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            await _httpClient.SendAsync(request);
        }
        public async Task<T> PostWithOutToken<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            var httpResponse = await _httpClient.SendAsync(request);
            return await httpResponse.Content.ReadFromJsonAsync<T>();
        }
        public async Task PostWithOutToken(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            await _httpClient.SendAsync(request);
        }
        public async Task<T> Post<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            var savedToken = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken.JwtToken);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            var httpResponse = await _httpClient.SendAsync(request);
            return await httpResponse.Content.ReadFromJsonAsync<T>();
        }

        public async Task Put(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            var savedToken = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken.JwtToken);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            await _httpClient.SendAsync(request);

        }

        public async Task<T> Put<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            var savedToken = await _localStorageService.GetItemAsync<Account>("account");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken.JwtToken);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            var httpResponse = await _httpClient.SendAsync(request);
            return await httpResponse.Content.ReadFromJsonAsync<T>();
        }
    }
}
