﻿using AutoMapper;
using LitBlog.BLL.Services.Interfaces;
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
        public async Task SendMessageAsync(string message, string userName)
        {
            await Clients.All.SendAsync("ReceiveMessage", message, userName);
        }
        public async Task ChatNotificationAsync(string message, string receiverUserId, string senderUserId)
        {
            await Clients.All.SendAsync("ReceiveChatNotification", message, receiverUserId, senderUserId);
        }
        public override async Task OnConnectedAsync()
        {
            var contextUser = Context.User;
            var userEmail = contextUser.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _accountService.GetAccountByEmailAsync(userEmail);
            var existConnecting = await _connectionService.GetClientByUserIdAsync(user.Id);
            if (existConnecting != null)
            {
                var connectionsModel = new ConnectionViewModel()
                {
                    ConnectedAt = DateTime.Now,
                    IsOnline = existConnecting.IsOnline,
                    ConnectionId = Context.ConnectionId,
                    UserAccount = user.Id
                };
                var mappingModel = _mapper.Map<ConnectionsDto>(connectionsModel);
                await _connectionService.UpdateConnection(mappingModel);
                await _connectionService.DeleteConnectionAsync(existConnecting.ConnectionId);
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
                await _connectionService.CreateConnectionAsync(mappingModel);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var contextUser = Context.User;
            var userEmail = contextUser.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _accountService.GetAccountByEmailAsync(userEmail);
            var existConnecting = await _connectionService.GetClientByUserIdAsync(user.Id);
            existConnecting.DisconnectedAt = DateTime.Now;
            existConnecting.IsOnline = false;
            await _connectionService.UpdateConnection(existConnecting);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
