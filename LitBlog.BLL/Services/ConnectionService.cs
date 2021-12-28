﻿using AutoMapper;
using LitChat.BLL.ModelsDto;
using LitChat.BLL.Services.Interfaces;
using LitChat.DAL.Models;
using LitChat.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.BLL.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly IConnectionRepository _connectionRepository;
        private readonly IMapper _mapper;
        public ConnectionService(IConnectionRepository connectionRepository, IMapper mapper)
        {
            _connectionRepository = connectionRepository;
            _mapper = mapper;
        }
        public async Task CreateConnectionAsync(ConnectionsDto connections)
        {
            if (connections is null)
            {
                throw new ApplicationException("null connection");
            }
            var mappingModel = _mapper.Map<Connections>(connections);
            await _connectionRepository.CreateConnection(mappingModel);
        }

        public async Task DeleteConnectionAsync(string ConnectionId)
        {
            await _connectionRepository.DeleteConnection(ConnectionId);
        }

        public async Task<IEnumerable<ConnectionsDto>> GetAllClientsAsync()
        {
            return _mapper.Map<IEnumerable<ConnectionsDto>>(await _connectionRepository.GetAllClients());
        }

        public async Task<ConnectionsDto> GetClientByUserIdAsync(int UserId)
        {
            var connections = await _connectionRepository.GetClientByConnectionId(UserId);
            return _mapper.Map<ConnectionsDto>(connections);
        }

        public async Task UpdateConnection(ConnectionsDto connections)
        {
            if (connections is null)
            {
                throw new ApplicationException("null connection");
            }
            var mappingModel = _mapper.Map<Connections>(connections);
            await _connectionRepository.UpdateConnection(mappingModel);
        }
    }
}
