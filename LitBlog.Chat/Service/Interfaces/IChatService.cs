using LitBlog.Chat.Data.Entitites;

namespace LitBlog.Chat.Service.Interfaces
{
    public interface IChatService
    {
        public Task GetConversationAsync(string userId, string contactId);
        public Task SaveMessageAsync(string userId, string ContactId, ChatMessage chatMessage);
    }
}
