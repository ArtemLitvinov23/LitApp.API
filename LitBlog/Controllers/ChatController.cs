using AutoMapper;
using LitBlog.API.Helpers;
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

        [HttpPost]
        public async Task<IActionResult> SaveMessageAsync(ChatMessageModel message)
        {
            var currentUserId = IdContext.GetUserId(HttpContext);
            var messageDto = _mapper.Map<ChatMessageDto>(message);
            await _chatService.SaveMessageAsync(currentUserId,messageDto);
            return Ok();
        }

        [HttpGet("{contactId}")]
        public async Task<IActionResult> GetConversationAsync(int contactId)
        {
            var currentUserId = IdContext.GetUserId(HttpContext);
            var result = await _chatService.GetConversationAsync(currentUserId, contactId);
            return Ok(result);
        }
    }
}
