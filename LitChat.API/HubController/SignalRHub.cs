using AutoMapper;
using LitChat.API.Models;
using LitChat.BLL.ModelsDto;
using LitChat.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LitChat.API.HubController
{
    [Authorize]
    public class SignalRHub : Hub
    {
        private readonly IConnectionService _connectionService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public SignalRHub(IConnectionService connectionService, IAccountService accountService, IMapper mapper)
        {
            _connectionService = connectionService;
            _accountService = accountService;
            _mapper = mapper;
        }
        public async Task SendMessageAsync(ChatMessageModel message, string userName)
        {
            await Clients.All.SendAsync("ReceiveMessage", message, userName);
        }
        public async Task ChatNotificationAsync(string message, string receiverUserId, string senderUserId)
        {
            await Clients.All.SendAsync("ChatNotification", message, receiverUserId, senderUserId);
        }
        public override async Task OnConnectedAsync()
        {
            var user = await GetUserAsync();
            var connection = await _connectionService.GetConnectionForUserAsync(user.Id);
            if (connection == null)
            {
                var connectionsModel = new ConnectionViewModel()
                {
                    ConnectedAt = DateTime.Now,
                    IsOnline = true,
                    ConnectionId = Context.ConnectionId,
                    UserAccount = user.Id
                };
                var mappingModel = _mapper.Map<ConnectionsDto>(connectionsModel);
                await _connectionService.CreateConnectionAsync(mappingModel);
            }
            else
            {
                var connectionsModel = new ConnectionViewModel()
                {
                    ConnectedAt = DateTime.Now,
                    IsOnline = true,
                    ConnectionId = Context.ConnectionId,
                    UserAccount = user.Id
                };
                var mappingModel = _mapper.Map<ConnectionsDto>(connectionsModel);
                await _connectionService.UpdateConnection(mappingModel);
                await _connectionService.DeleteConnectionAsync(connection.UserAccount);
            }
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = await GetUserAsync();
            await _connectionService.CloseConnection(user.Id);
            await base.OnDisconnectedAsync(exception);
        }
        private async Task<AccountResponseDto> GetUserAsync()
        {
            var contextUser = Context.User;
            var userEmail = contextUser.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _accountService.GetAccountByEmailAsync(userEmail);
            return user;
        }
    }
}
