using LitBlog.DAL;
using LitChat.DAL.Models;
using LitChat.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.DAL.Repositories
{
    public class ConnectionRepository : IConnectionRepository
    {
        private readonly BlogContext _blogContext;
        public ConnectionRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }
        public async Task CreateConnection(Connections connections)
        {
            await _blogContext.Connections.AddAsync(connections);
            await _blogContext.SaveChangesAsync();
        }

        public async Task DeleteConnection(string ConnectionId)
        {
            var user = await _blogContext.Connections.FirstOrDefaultAsync(x => x.ConnectionId == ConnectionId);
            _blogContext.Connections.Remove(user);
            await _blogContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Connections>> GetAllClients()
        {
            return await _blogContext.Connections.ToListAsync();
        }
        public async Task<Connections> GetClientByConnectionId(int UserId) => await _blogContext.Connections.SingleOrDefaultAsync(x => x.UserAccount == UserId);

        public async Task UpdateConnection(Connections connections)
        {
            _blogContext.Connections.Update(connections);
            await _blogContext.SaveChangesAsync();
        }
    }
}
