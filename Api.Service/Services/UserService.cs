using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        // Declara um repositório de usuário e um mapeador.
        private IRepository<UserEntity> _repository;
        private readonly IMapper _mapper;

        // Construtor que inicializa o repositório e o mapeador com as dependências injetadas.
        public UserService(IRepository<UserEntity> repository, IMapper mapper)
        {
            _repository = repository; // Inicializa o repositório de usuário.
            _mapper = mapper; // Inicializa o mapeador.
        }

        // Método assíncrono que deleta um usuário pelo ID.
        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id); // Chama o método de exclusão do repositório.
        }

        // Método assíncrono que obtém um usuário pelo ID.
        public async Task<UserDto> Get(Guid id)
        {
            var entity = await _repository.SelectAsync(id); // Busca o usuário pelo ID.
            return _mapper.Map<UserDto>(entity); // Mapeia a entidade para um DTO e a retorna.
        }

        // Método assíncrono que obtém todos os usuários.
        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var listEntity = await _repository.SelectAsync(); // Busca todos os usuários.
            return _mapper.Map<IEnumerable<UserDto>>(listEntity); // Mapeia a lista de entidades para uma lista de DTOs e a retorna.
        }

        // Método assíncrono que cria um novo usuário.
        public async Task<UserDtoCreateResult> Post(UserDtoCreat user)
        {
            var model = _mapper.Map<UserModel>(user); // Mapeia o DTO para um modelo de usuário.
            var entity = _mapper.Map<UserEntity>(model); // Mapeia o modelo para uma entidade de usuário.
            var result = await _repository.InsertAsync(entity); // Insere a entidade no repositório.
            return _mapper.Map<UserDtoCreateResult>(result); // Mapeia o resultado para um DTO de criação e o retorna.
        }

        // Método assíncrono que atualiza um usuário.
        public async Task<UserDtoUpdateResult> Put(UserDtoUpdate user)
        {
            var model = _mapper.Map<UserModel>(user); // Mapeia o DTO para um modelo de usuário.
            var entity = _mapper.Map<UserEntity>(model); // Mapeia o modelo para uma entidade de usuário.
            var result = await _repository.UpdateAsync(entity); // Atualiza a entidade no repositório.
            return _mapper.Map<UserDtoUpdateResult>(result); // Mapeia o resultado para um DTO de atualização e o retorna.
        }

    }
}