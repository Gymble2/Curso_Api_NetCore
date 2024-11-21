using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class CepMap  : IEntityTypeConfiguration<CepEntity>
    {
        public void Configure(EntityTypeBuilder<CepEntity> builder)
        {
            //Configura a entidade UserEntity para mapear para a tabela User no banco de dados.
            builder.ToTable("Cep");
            //Define a propriedade Id como a chave primária da entidade UserEntity.
            builder.HasKey(k => k.Id);
            //Cria um índice na propriedade Email e garante que esse índice é único, ou seja, não podem existir dois usuários com o mesmo email no banco de dados.
            builder.HasIndex(e => e.Cep)
                    .IsUnique();

            builder.HasOne(c => c.Municipio)
                   .WithMany(m => m.Ceps);
        }
    }
}