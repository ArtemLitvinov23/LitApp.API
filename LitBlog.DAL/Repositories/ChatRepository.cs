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
            var message = await _blogContext.Messages.AsQueryable()
                .Include(a => a.FromUser)
                .Include(a => a.ToUser)
                .Where(h => (h.FromUserId == contactId && h.ToUserId == userId) || (h.FromUserId == userId && h.ToUserId == contactId))
                .Where(x => x.CreatedDate > System.DateTime.Now.AddDays(-1))
                .OrderBy(a => a.CreatedDate)
                .Select(x => new ChatMessages
                  {
                 FromUserId = x.FromUserId,
                 Message = x.Message,
                 Id = x.Id,
                 ToUserId = x.ToUserId,
                 ToUser = x.ToUser,
                 FromEmail = x.FromEmail,
                 ToEmail = x.ToEmail,
                 FromUser = x.FromUser,
                 CreatedDate = x.CreatedDate,
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
