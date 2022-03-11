using AutoMapper;
using LitChat.API.Models;
using LitChat.BLL.Exceptions;
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
        public SignalRHub(
            IConnectionService connectionService,
            IAccountService accountService,
            IMapper mapper)
        {
            _connectionService = connectionService;
            _accountService = accountService;
            _mapper = mapper;
        }
        public async Task SendMessageAsync(ChatMessageModel message, string userName)
        {
            try
            {
                await Clients.All.SendAsync("ReceiveMessage", message, userName);
            }
            catch (Exception e)
            {
                throw new InternalServerException(e.Message);
            }
        }

        public async Task SendFriendRequest(FriendRequestViewModel friendRequest)
        {
            try
            {
                await Clients.All.SendAsync("ReceiveFriendRequest", friendRequest);
            }
            catch (Exception e)
            {
                throw new InternalServerException(e.Message);
            }
        }
        public async Task FriendNotificationAsync(string message, string senderUserId, string receiverUserId)
        {
            try
            {
                await Clients.All.SendAsync("FriendNotification", message, senderUserId, receiverUserId);
            }
            catch (Exception e)
            {
                throw new InternalServerException(e.Message);
            }
        }

        public async Task ChatNotificationAsync(string message, string receiverUserId, string senderUserId)
        {
            try
            {
                await Clients.All.SendAsync("ChatNotification", message, receiverUserId, senderUserId);
            }
            catch (Exception e)
            {
                throw new InternalServerException(e.Message);
            }
        }
        public override async Task OnConnectedAsync()
        {
            try
            {
                var user = await GetUserAsync();
                var connectionInfo = await _connectionService.GetConnectionForUserAsync(user.Id);
                if (connectionInfo == null)
                {
                    var newConnections = new ConnectionViewModel()
                    {
                        ConnectedAt = DateTime.Now,
                        IsOnline = true,
                        ConnectionId = Context.ConnectionId,
                        UserAccount = user.Id
                    };
                    var conectionDto = _mapper.Map<ConnectionsDto>(newConnections);
                    await _connectionService.CreateConnectionAsync(conectionDto);
                }
                var updateConnection = await _connectionService.GetConnectionById(connectionInfo.Id);

                updateConnection.Id = connectionInfo.Id;
                updateConnection.ConnectionId = Context.ConnectionId;
                updateConnection.ConnectedAt = DateTime.Now;
                updateConnection.IsOnline = true;

                var updateConnectionDto = _mapper.Map<ConnectionsDto>(updateConnection);

                await _connectionService.UpdateConnection(updateConnectionDto);
                await base.OnConnectedAsync();
            }
            catch (Exception e)
            {
                throw new InternalServerException(e.Message);
            }
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
