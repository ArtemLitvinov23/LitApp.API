using LitApp.DAL;
using LitApp.DAL.Models;
using LitApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LitApp.DAL.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly BlogContext _blogContext;
        public ChatRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task<IEnumerable<ChatMessages>> GetFullChatHistory(int userId, int contactId)
        {
            var message = await _blogContext.Messages
                .Include(a => a.FromUser)
                .Include(a => a.ToUser)
                .Where(h => h.FromUserId == contactId && h.ToUserId == userId || h.FromUserId == userId && h.ToUserId == contactId)
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
                    CreatedDate = x.CreatedDate.ToLocalTime(),
                }).ToListAsync();
            return message;
        }

        public async Task RemoveChatHistory(int userId, int contactId)
        {
            var findChat = await _blogContext.Messages.Where(x => x.FromUserId == userId && x.ToUserId == contactId || x.FromUserId == contactId && x.ToUserId == userId).ToListAsync();
            _blogContext.Messages.RemoveRange(findChat);
            await _blogContext.SaveChangesAsync();
        }
        public async Task RemoveMessage(int messageId)
        {
            var message = await _blogContext.Messages.FindAsync(messageId);
            _blogContext.Messages.Remove(message);
            await _blogContext.SaveChangesAsync();
        }
        public async Task RemoveAllMyMessages(int userId)
        {
            var findChat = await _blogContext.Messages.Where(x => x.FromUserId == userId || x.ToUserId == userId).ToListAsync();
            _blogContext.Messages.RemoveRange(findChat);
            await _blogContext.SaveChangesAsync();
        }
        public async Task SaveMessageAsync(ChatMessages message)
        {
            await _blogContext.Messages.AddAsync(message);
            await _blogContext.SaveChangesAsync();
        }
    }
}
