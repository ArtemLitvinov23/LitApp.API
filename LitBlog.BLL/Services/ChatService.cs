using AutoMapper;
using LitBlog.BLL.ModelsDto;
using LitBlog.DAL.Models;
using LitBlog.DAL.Repositories;
using System;
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

        public UsersResponseDto GetAllUsers()
        {
            var allAccount = _accountRepository.GetAllAccounts();
            return _mapper.Map<UsersResponseDto>(allAccount);
            
        }

        public async Task GetConversationAsync(int userId, int contactId)
        {
            await _chatRepository.GetConversationAsync(userId, contactId);
        }

        public UsersResponseDto GetUser(int userId)
        {
            var account =  _accountRepository.GetAllAccounts().Where(x => x.Id == userId).FirstOrDefault();
            var user = _mapper.Map<UsersResponseDto>(account);
            return user;

        }

        public async Task SaveMessage(int userId,ChatMessageDto chatMessage)
        {
            chatMessage.FromUserId = userId;
            chatMessage.CreatedDate = DateTime.UtcNow;
            chatMessage.ToUser = _mapper.Map<ApplicationUserDto>(_accountRepository.GetAllAccounts().Where(user => user.Id == chatMessage.ToUserId).FirstOrDefault());
            var messageDto = _mapper.Map<ChatMessage>(chatMessage);
            await _chatRepository.SaveMessageAsync(messageDto);
        }
    }
}
