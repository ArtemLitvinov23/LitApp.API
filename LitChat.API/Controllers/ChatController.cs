using AutoMapper;
using LitChat.API.Models;
using LitChat.BLL.ModelsDto;
using LitChat.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IMapper _mapper;

        public ChatController(
            IChatService chatService,
            IMapper mapper)
        {
            _chatService = chatService;
            _mapper = mapper;
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> SaveMessageAsync(int userId, ChatMessageModel message)
        {
            var messageDto = _mapper.Map<ChatMessagesDto>(message);
            await _chatService.SaveMessageAsync(userId, messageDto);
            return Ok();
        }

        [HttpGet("{userId}/{contactId}")]
        public async Task<List<ChatMessagesResponseViewModel>> GetConversationAsync(int userId, int contactId)
        {
            var message = await _chatService.GetLastFourMessagesAsync(userId, contactId);
            var result = _mapper.Map<List<ChatMessagesResponseViewModel>>(message);
            return result;
        }
        [HttpGet("full/{userId}/{contactId}")]
        public async Task<List<ChatMessagesResponseViewModel>> GetFullChatHistory(int userId, int contactId)
        {
            var message = await _chatService.GetFullHistoryMessagesAsync(userId, contactId);
            var result = _mapper.Map<List<ChatMessagesResponseViewModel>>(message);
            return result;
        }
        [HttpDelete("{userId}/{contactId}")]
        public async Task<IActionResult> DeleteChatHistory(int userId, int contactId)
        {
            await _chatService.RemoveChatHistory(userId, contactId);
            return Ok();
        }
        [HttpDelete("{messageId}")]
        public async Task<IActionResult> DeleteChatHistory(int messageId)
        {
            await _chatService.RemoveMessage(messageId);
            return Ok();
        }
    }
}
