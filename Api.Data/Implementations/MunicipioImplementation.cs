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
    public class MunicipioImplementation : BaseRepository<MunicipioEntity>, IMunicipioRepository
    {
                // Define um DbSet para UserEntity.
        DbSet<MunicipioEntity> _dataset;

        // Construtor que inicializa o contexto e o DbSet.
        public MunicipioImplementation(MyContext context) : base(context)
        {
            // Atribui o contexto ao DbSet.
            _dataset = context.Set<MunicipioEntity>();
        }

        public async Task<MunicipioEntity> GetCompletByCodigoDoIBGE(int CodIBGE)
        {
          return await _dataset.Include(m => m.Uf)
                            .FirstOrDefaultAsync(m => m.CodIBGE.Equals(CodIBGE));
                            
        }

        public async Task<MunicipioEntity> GetCompletById(Guid Id)
        {
            return await _dataset.Include(m => m.Uf)
                            .FirstOrDefaultAsync(m => m.Id.Equals(Id));                       
        }
    }
}