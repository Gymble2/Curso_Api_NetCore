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
    public class CepImplementation : BaseRepository<CepEntity>, ICepRepository
    {
                // Define um DbSet para UserEntity.
        DbSet<CepEntity> _dataset;

        // Construtor que inicializa o contexto e o DbSet.
        public CepImplementation(MyContext context) : base(context)
        {
            // Atribui o contexto ao DbSet.
            _dataset = context.Set<CepEntity>();
        }

        public async Task<CepEntity> SelectAsync(string cep)
        {
            return await _dataset.Include(c => c.Municipio)
                            .ThenInclude(m => m.Uf)
                            .SingleOrDefaultAsync(u => u.Cep.Equals(cep));
        }
    }
}