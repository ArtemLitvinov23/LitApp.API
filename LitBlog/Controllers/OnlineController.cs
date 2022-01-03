using AutoMapper;
using LitChat.API.Models;
using LitChat.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineController : ControllerBase
    {
        private readonly IConnectionService _connectionService;
        private readonly IMapper _mapper;
        public OnlineController(IConnectionService connectionService, IMapper mapper)
        {
            _connectionService = connectionService;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<ConnectionsResponseViewModel>>> GetConnectedClient()
        {
           var allClients = await _connectionService.GetAllClientsAsync();
           var mappingModel = _mapper.Map<List<ConnectionsResponseViewModel>>(allClients);
           return Ok(mappingModel);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ConnectionsResponseViewModel>> GetConnectedClient(int id)
        {
            var allClients = await _connectionService.GetClientByUserIdAsync(id);
            var mappingModel = _mapper.Map<ConnectionsResponseViewModel>(allClients);
            return Ok(mappingModel);
        }
        [HttpPatch("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ConnectionsResponseViewModel>> DisconnectClient(int id)
        {
            await _connectionService.CloseConnection(id);
            return Ok();
        }
    }
}
