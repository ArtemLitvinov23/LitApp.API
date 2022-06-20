using AutoMapper;
using LitApp.BLL.Exceptions;
using LitApp.BLL.ModelsDto;
using LitApp.BLL.Services.Interfaces;
using LitApp.DAL.Models;
using LitApp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LitApp.BLL.Services
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
        public async Task SaveMessageAsync(int userId, ChatMessagesDto chatMessage)
        {
            var fromUser = await _accountRepository.GetAccountByIdAsync(userId);

            chatMessage.FromUserId = userId;
            chatMessage.FromEmail = fromUser.Email;

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
                throw new InternalServerException();

            await _chatRepository.RemoveChatHistory(userId, contactId);
        }
    }
}
