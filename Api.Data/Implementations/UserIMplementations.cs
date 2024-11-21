using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    // Implementa o repositório de usuários herdando de BaseRepository<UserEntity> e implementando IUSerRepository.
    public class UserImplementations : BaseRepository<UserEntity>, IUSerRepository
    {
        // Define um DbSet para UserEntity.
        DbSet<UserEntity> _dataset;

        // Construtor que inicializa o contexto e o DbSet.
        public UserImplementations(MyContext context) : base(context)
        {
            // Atribui o contexto ao DbSet.
            _dataset = context.Set<UserEntity>();
        }

        // Método assíncrono que busca um usuário pelo email.
        public async Task<UserEntity> FindByLogin(string email)
        {
            // Retorna o primeiro usuário encontrado cujo email corresponde ao email fornecido.
            return await _dataset.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }
    }

}