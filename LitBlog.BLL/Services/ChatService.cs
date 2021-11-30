using AutoMapper;
using LitBlog.BLL.ModelsDto;
using LitBlog.DAL.Models;
using LitBlog.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
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

        public ChatService(IChatRepository chatRepository,IAccountRepository accountRepository, IMapper mapper)
        {
            _chatRepository = chatRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<List<UsersResponseDto>> GetAllUsersAsync()
        {
            var allAccount = await _accountRepository.GetAllAccounts().ToListAsync();
            return _mapper.Map<List<UsersResponseDto>>(allAccount);
            
        }

        public async Task GetConversationAsync(string userId, string contactId)
        {
            await _chatRepository.GetConversationAsync(userId, contactId);
        }

        public async Task<UsersResponseDto> GetUserAsync(string userId)
        {
            var account = await _accountRepository.GetAllAccounts().Where(x => x.Id == Convert.ToInt32(userId)).FirstOrDefaultAsync();
            var user = _mapper.Map<UsersResponseDto>(account);
            return user;

        }

        public async Task SaveMessageAsync(string userId,ChatMessageDto chatMessage)
        {
            chatMessage.FromUserId = userId;
            chatMessage.CreatedDate = DateTime.UtcNow;
            chatMessage.ToUser = _mapper.Map<ApplicationUserDto>(_accountRepository.GetAllAccounts().Where(user => user.Id == Convert.ToInt32(chatMessage.ToUserId)).FirstOrDefault());
            var messageDto = _mapper.Map<ChatMessage>(chatMessage);
            await _chatRepository.SaveMessageAsync(messageDto, userId);
        }
    }
}
