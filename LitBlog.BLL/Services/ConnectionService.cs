using AutoMapper;
using LitChat.BLL.ModelsDto;
using LitChat.DAL.Models;
using LitChat.DAL.Repositories;
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

        public async Task CloseConnection(int accountId)
        {
            var user = await _connectionRepository.GetClientById(accountId);
            user.DisconnectedAt = DateTime.Now;
            user.IsOnline = false;
            var mappingModel = _mapper.Map<Connections>(user);
            await _connectionRepository.UpdateConnection(mappingModel);
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

        public async Task DeleteConnectionAsync(int UserId)
        {
            var connections = await _connectionRepository.GetClientById(UserId);
            await _connectionRepository.DeleteConnection(connections.ConnectionId);
        }

        public async Task<IEnumerable<ConnectionsResponseDto>> GetAllClientsAsync()
        {
            return _mapper.Map<IEnumerable<ConnectionsResponseDto>>(await _connectionRepository.GetAllClients());
        }

        public async Task<ConnectionsResponseDto> GetClientByUserIdAsync(int UserId)
        {
            var connections = await _connectionRepository.GetClientById(UserId);
            return _mapper.Map<ConnectionsResponseDto>(connections);
        }

        public async Task<ConnectionsDto> GetConnectionForUserAsync(int accountId)
        {
            return _mapper.Map<ConnectionsDto>(await _connectionRepository.GetConnectionForUserAsync(accountId));
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
