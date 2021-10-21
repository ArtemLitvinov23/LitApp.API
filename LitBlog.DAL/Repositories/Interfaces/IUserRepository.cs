using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LitBlog.DAL.Models;

namespace LitBlog.DAL.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUser();
        Task<User> GetUserById(int id);
        Task AddUser(User user);
        Task UpdateUser(User user);
        void Delete(int id);
    }
}