using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.Blazor.Services.Interfaces
{
    public interface IHttpServiceGeneric
    {
        Task<T> Get<T>(string uri);
        Task<List<T>> GetAll<T>(string uri);
        Task Post(string uri, object value);
        Task<T> Post<T>(string uri, object value);
        Task<T> PostWithOutToken<T>(string uri, object value);
        Task PostWithOutToken(string uri, object value);
        Task Put(string uri, object value);
        Task<T> Put<T>(string uri, object value);
        Task Patch(string uri, object value);
        Task Patch(string uri);
        Task Delete(string uri);

    }
}
