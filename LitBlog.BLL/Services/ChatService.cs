using AutoMapper;
using LitBlog.BLL.ModelsDto;
using LitBlog.DAL.Models;
using LitBlog.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitBlog.BLL.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public ChatService(IChatRepository chatRepository,IAccountRepository accountRepository, IMapper mapper)
        {
            _chatRepository = chatRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<List<ChatMessageDto>> GetConversationAsync(int userId, int contactId) => _mapper.Map<List<ChatMessageDto>>(await _chatRepository.GetConversationAsync(userId, contactId));
       
        public async Task SaveMessageAsync(int userId,ChatMessageDto chatMessage)
        {
            chatMessage.FromUserId = userId;
            var fromUser = await _accountRepository.GetAccountByIdAsync(userId);
            chatMessage.FromUser = _mapper.Map<ApplicationUserDto>(fromUser.Email);
            chatMessage.CreatedDate = DateTime.UtcNow;
            var toUser = await _accountRepository.GetAccountByIdAsync(chatMessage.ToUserId);
            var userEmail = _mapper.Map<ApplicationUserDto>(toUser.Email);
            chatMessage.ToUser = userEmail;
            var messageDto = _mapper.Map<ChatMessage>(chatMessage);
            await _chatRepository.SaveMessageAsync(messageDto);
        }
    }
}
