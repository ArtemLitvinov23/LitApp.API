using LitChat.Blazor.Models;
using LitChat.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.Blazor.Services
{

    public class AccountService : IAccountService
    {
        public Account Account { get; private set; }
        private readonly IHttpServiceGeneric _httpService;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorageService;

        public AccountService(IHttpServiceGeneric httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService)
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }
        public async Task<Users> GetUserDetailsAsync(string userId) => await _httpService.Get<Users>($"api/Account/get-user/{userId}");
        public async Task<List<Users>> GetAllUsersAsync(string currentUserId) => await _httpService.GetAll<Users>($"api/Account/get-users/{currentUserId}");
        public async Task Initialize()
        {
            Account = await _localStorageService.GetItemAsync<Account>("account");
        }
        public async Task Login(AuthAccount model)
        {
            Account = await _httpService.PostWithOutToken<Account>("api/Account/sign-in", model);
            await _localStorageService.SetItem("account", Account);
        }
        public async Task Register(RegisterAccount model) => await _httpService.PostWithOutToken("api/Account/sign-up", model);
        public async Task Verify(VerifyAccount model) => await _httpService.PostWithOutToken("api/Account/verify", model);
        public async Task ForgotPassword(ForgotPassword model) => await _httpService.PostWithOutToken("api/Account/forgot-password", model);
        public async Task ResetPassword(ResetPassword model) => await _httpService.PostWithOutToken("api/Account/forgot-password", model);
        public async Task Logout()
        {
            Account = null;
            await _localStorageService.RemoveItem("account");
            _navigationManager.NavigateTo("/account/login");
        }
        public async Task Update(int userId, UpdateAccount model)
        {
            await _httpService.Put($"api/Account/{userId}", model);
            Account.FirstName = model.FirstName;
            Account.LastName = model.LastName;
            await _localStorageService.SetItem("Token", Account.JwtToken);
        }
        public async Task Delete(int id)
        {
            await _httpService.Delete($"api/Account/{id}");

            if (id == Account.Id)
            {
                await Logout();
            }
        }
        public async Task<Account> GetUserDataFromLocalStorage() => await _localStorageService.GetItemAsync<Account>("account");

    }
}
