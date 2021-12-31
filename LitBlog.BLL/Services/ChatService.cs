using AutoMapper;
using LitBlog.BLL.ModelsDto;
using LitBlog.DAL.Models;
using LitBlog.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LitBlog.BLL.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public ChatService(
            IChatRepository chatRepository,
            IAccountRepository accountRepository,
            IMapper mapper)
        {
            _chatRepository = chatRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        public async Task<List<ChatMessagesDto>> GetLastFourMessagesAsync(int userId, int contactId)
        {
            var messages = await _chatRepository.GetFullChatHistory(userId, contactId);
            var result = _mapper.Map<List<ChatMessagesDto>>(messages);
            return result.TakeLast(4).ToList();
        }
        public async Task<List<ChatMessagesDto>> GetFullHistoryMessagesAsync(int userId, int contactId)
        {
            var messages = await _chatRepository.GetFullChatHistory(userId, contactId);
            var result = _mapper.Map<List<ChatMessagesDto>>(messages);
            return result;
        }
        public async Task SaveMessageAsync(int userId,ChatMessagesDto chatMessage)
        {
            chatMessage.FromUserId = userId;
            var fromUser = await _accountRepository.GetAccountByIdAsync(userId);
            chatMessage.FromEmail = fromUser.Email;
            chatMessage.CreatedDate = DateTime.UtcNow;
            var messageDto = _mapper.Map<ChatMessages>(chatMessage);
            await _chatRepository.SaveMessageAsync(messageDto);
        }

        public async Task RemoveMessage(int messageId)
        {
            await _chatRepository.RemoveMessage(messageId);
        }

        public async Task RemoveChatHistory(int userId, int contactId)
        {
            var user = _accountRepository.GetAccountByIdAsync(userId);
            var contact = _accountRepository.GetAccountByIdAsync(contactId);
            if (user == null || contact == null)
            {
                throw new ApplicationException("Can't find this users");
            }
            await _chatRepository.RemoveChatHistory(userId, contactId);
        }
    }
}
