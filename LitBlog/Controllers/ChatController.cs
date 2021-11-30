using AutoMapper;
using LitBlog.API.Models;
using LitBlog.BLL.ModelsDto;
using LitBlog.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public ChatController(IChatService chatService, IMapper mapper)
        {
            _chatService = chatService;
            _mapper = mapper;
        }


        [HttpGet("users")]
        public async Task<IActionResult> GetAllUser()
        {
            var accounts = await _chatService.GetAllUsersAsync();
            return Ok(accounts);
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUserDetails(string id)
        {
            var accounts = await _chatService.GetUserAsync(id);
            return Ok(accounts);
        }

        [HttpPost("{currentUserId}")]
        public async Task<IActionResult> SaveMessageAsync(ChatMessageModel message,string currentUserId)
        {
            var messageDto = _mapper.Map<ChatMessageDto>(message);
            await _chatService.SaveMessageAsync(currentUserId,messageDto);
            return Ok();
        }

        [HttpGet("{userId}/{contactId}")]
        public async Task<IActionResult> GetConversationAsync(string userId ,string contactId)
        {
            await _chatService.GetConversationAsync(userId, contactId);
            return Ok();
        }
    }
}
