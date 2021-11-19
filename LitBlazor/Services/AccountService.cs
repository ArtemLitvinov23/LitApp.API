using LitBlazor.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using LitBlazor.Services.Interfaces;

namespace LitBlazor.Services
{
 
    public class AccountService : IAccountService
    {
        public Account Account { get; private set; }

        private readonly IHttpService _httpService;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorageService;

        public AccountService(IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService)
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

        public async Task Delete(int id)
        {
            await _httpService.Delete($"api/Account/{id}");

            if (id == Account.Id)
            {
                await Logout();
            }
        }

        public async Task ForgotPassword(ForgotPassword model)
        {
            await _httpService.Post("api/Account/forgot-password", model);
        }

        public async Task<IList<Account>> GetAll()
        {
            return await _httpService.Get<IList<Account>>("api/Account");
        }

        public async Task<Account> GetById(int id)
        {
           return await _httpService.Get<Account>($"api/Account/{id}");
        }

        public async Task Initialize()
        {
            Account = await _localStorageService.GetItemAsync<Account>("account");
        }

        public async Task Login(AuthAccount model)
        {
            Account = await _httpService.Post<Account>("api/Account/auth", model);
            await _localStorageService.SetItem("account", Account);
        }

        public async Task Logout()
        {
            Account = null;
            await _localStorageService.RemoveItem("account");
            _navigationManager.NavigateTo("api/Account/auth");
        }

        public async Task Register(RegisterAccount model)
        {
            await _httpService.Post("api/Account/register", model);
        }

        public async Task Update(UpdateAccount model)
        {
            await _httpService.Put("api/Account", model);

            Account.UserName = model.UserName;
            Account.LastName = model.LastName;
            await _localStorageService.SetItem("Token", Account.jwtToken);
        }

        public async Task Verify(VerifyAccount model)
        {
            await _httpService.Post("api/Account/verify", model);
        }

        public async Task ResetPassword(ResetPassword model)
        {
            await _httpService.Post("api/Account/forgot-password", model);
        }

    }
}
