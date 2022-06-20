using AutoMapper;
using LitApp.BLL.ModelsDto;
using LitApp.BLL.Services.Interfaces;
using LitApp.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveMessageAsync(int userId, ChatMessageModel message)
        {
            var messageDto = _mapper.Map<ChatMessagesDto>(message);

            await _chatService.SaveMessageAsync(userId, messageDto);

            return Ok();
        }

        [HttpGet("{userId}/{contactId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChatMessagesResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<ChatMessagesResponseViewModel>>> GetConversationAsync(int userId, int contactId)
        {
            var message = await _chatService.GetLastFourMessagesAsync(userId, contactId);

            var result = _mapper.Map<List<ChatMessagesResponseViewModel>>(message);

            return Ok(result);
        }

        [HttpGet("full/{userId}/{contactId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChatMessagesResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<ChatMessagesResponseViewModel>>> GetFullChatHistory(int userId, int contactId)
        {
            var message = await _chatService.GetFullHistoryMessagesAsync(userId, contactId);

            var result = _mapper.Map<List<ChatMessagesResponseViewModel>>(message);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpDelete("{userId}/{contactId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteChatHistory(int userId, int contactId)
        {
            await _chatService.RemoveChatHistory(userId, contactId);

            return Ok();
        }

        [HttpDelete("{messageId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteChatHistory(int messageId)
        {
            await _chatService.RemoveMessage(messageId);

            return Ok();
        }
    }
}
