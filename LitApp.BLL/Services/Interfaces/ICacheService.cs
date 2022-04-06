using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitApp.BLL.Services.Interfaces
{
    public interface ICacheService<T>
    {
        Task<T> Get(int id);

        Task<List<T>> GetList();

        Task Set(T item);

        Task SetList(List<T> item);
    }
}
