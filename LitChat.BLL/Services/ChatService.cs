using AutoMapper;
using LitChat.BLL.Exceptions;
using LitChat.BLL.ModelsDto;
using LitChat.BLL.ModelsDTO;
using LitChat.BLL.Services.Interfaces;
using LitChat.DAL.Models;
using LitChat.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LitChat.BLL.Services
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
        public async Task<StatusEnum> SaveMessageAsync(int userId, ChatMessagesDto chatMessage)
        {
            var fromUser = await _accountRepository.GetAccountByIdAsync(userId);

            if (fromUser == null)
                return StatusEnum.BadRequest;

            chatMessage.FromUserId = userId;

            chatMessage.FromEmail = fromUser.Email;

            chatMessage.CreatedDate = DateTime.UtcNow;

            var messageDto = _mapper.Map<ChatMessages>(chatMessage);

            await _chatRepository.SaveMessageAsync(messageDto);

            return StatusEnum.OK;  
        }

        public async Task<StatusEnum> RemoveMessage(int messageId)
        {
            await _chatRepository.RemoveMessage(messageId);

            return StatusEnum.OK;
        }

        public async Task<StatusEnum> RemoveChatHistory(int userId, int contactId)
        {
            var user = _accountRepository.GetAccountByIdAsync(userId);

            var contact = _accountRepository.GetAccountByIdAsync(contactId);

            if (user == null || contact == null)
                return StatusEnum.BadRequest;

            await _chatRepository.RemoveChatHistory(userId, contactId);

            return StatusEnum.OK;
        }
    }
}
