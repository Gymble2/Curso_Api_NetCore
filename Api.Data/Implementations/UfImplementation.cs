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
    public class UfImplementation : BaseRepository<UfEntity>, IUfRepository
    {
                // Define um DbSet para UserEntity.
        DbSet<UfEntity> _dataset;

        // Construtor que inicializa o contexto e o DbSet.
        public UfImplementation(MyContext context) : base(context)
        {
            // Atribui o contexto ao DbSet.
            _dataset = context.Set<UfEntity>();
        }

    }
}