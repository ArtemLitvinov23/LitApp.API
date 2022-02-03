using System.Threading.Tasks;

namespace LitChat.Blazor.Services.Interfaces
{
    public interface ILocalStorageService
    {
        Task<T> GetItemAsync<T>(string key);
        Task SetItem<T>(string key, T value);
        Task RemoveItem(string key);
    }
}
