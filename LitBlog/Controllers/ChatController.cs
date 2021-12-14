using AutoMapper;
using LitBlog.API.Models;
using LitBlog.BLL.ModelsDto;
using LitBlog.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitBlog.API.Controllers
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
        public async Task<IActionResult> SaveMessageAsync(int userId,ChatMessageModel message)
        {
            var messageDto = _mapper.Map<ChatMessagesDto>(message);
            await _chatService.SaveMessageAsync(userId,messageDto);
            return Ok();
        }

        [HttpGet("{userId}/{contactId}")]
        public async Task<List<ChatMessageModel>> GetConversationAsync(int userId,int contactId)
        {
            var message = await _chatService.GetConversationAsync(userId, contactId);
            var result = _mapper.Map<List<ChatMessageModel>>(message);
            return result;
        }
    }
}
