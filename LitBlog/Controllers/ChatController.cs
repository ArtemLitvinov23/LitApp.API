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


        [HttpGet("User")]
        public IActionResult GetAllUser()
        {
            var accounts = _chatService.GetAllUsers();
            return Ok(accounts);
        }

        [HttpGet("User/{id}")]
        public IActionResult GetUserDetails(int id)
        {
            var accounts = _chatService.GetUser(id);
            return Ok(accounts);
        }

        [HttpPost]
        public async Task<IActionResult> SaveMessageAsync(ChatMessageModel message)
        {
            var userId = IdContext.GetUserId(HttpContext);
            var messageDto = _mapper.Map<ChatMessageDto>(message);
            await _chatService.SaveMessage(userId,messageDto);
            return Ok();
        }

        [HttpGet("{contactId}")]
        public async Task<IActionResult> GetConversationAsync(int contactId)
        {
            var userId = IdContext.GetUserId(HttpContext);
            await _chatService.GetConversationAsync(userId, contactId);
            return Ok();
        }
    }
}
