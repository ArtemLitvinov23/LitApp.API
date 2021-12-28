using AutoMapper;
using LitChat.API.Models;
using LitChat.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<ActionResult<IEnumerable<ConnectionsResponseViewModel>>> GetConnectedClient()
        {
           var allClients = await _connectionService.GetAllClientsAsync();
           var mappingModel = _mapper.Map<IEnumerable<ConnectionsResponseViewModel>>(allClients);
           return Ok(mappingModel);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ConnectionsResponseViewModel>> GetConnectedClient(int id)
        {
            var allClients = await _connectionService.GetClientByUserIdAsync(id);
            var mappingModel = _mapper.Map<ConnectionsResponseViewModel>(allClients);
            return Ok(mappingModel);
        }
    }
}
