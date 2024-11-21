using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class MunicipioMap : IEntityTypeConfiguration<MunicipioEntity>
    {
        public void Configure(EntityTypeBuilder<MunicipioEntity> builder)
        {
            //Configura a entidade UserEntity para mapear para a tabela User no banco de dados.
            builder.ToTable("Municipios");
            //Define a propriedade Id como a chave primária da entidade UserEntity.
            builder.HasKey(k => k.Id);
            //Cria um índice na propriedade Email e garante que esse índice é único, ou seja, não podem existir dois usuários com o mesmo email no banco de dados.
            builder.HasIndex(e => e.CodIBGE);

            builder.HasOne(e => e.Uf)
                   .WithMany(m => m.Municipios);
        }
    }
}