using LitBlog.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LitBlog.DAL.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly BlogContext _blogContext;
        public ChatRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task<List<ChatMessages>> GetConversationAsync(int userId,int contactId)
        {
            var message = await _blogContext.Messages
                .Where(h => (h.FromUserId == contactId && h.ToUserId == userId) || (h.FromUserId == userId && h.ToUserId == contactId))
                .OrderBy(a => a.CreatedDate)
                .Include(a => a.FromUser)
                .Include(a=>a.ToUser)
                .Select(x => new ChatMessages
                {
                    FromUserId = x.FromUserId,
                    Message = x.Message,
                    Id = x.Id,
                    ToUserId = x.ToUserId,
                    ToUser = x.ToUser,
                    FromUser = x.FromUser
                }).ToListAsync();
            return message;
        }

        public async Task SaveMessageAsync(ChatMessages message)
        {
            await _blogContext.Messages.AddAsync(message);
            await _blogContext.SaveChangesAsync();
        }
    }
}
