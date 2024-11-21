using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    //3° passo mapeia e trata as informações da tabela que for criada e aplica ela 
    //Define uma classe UserMap que implementa a interface IEntityTypeConfiguration<UserEntity>. Isso significa que esta classe configurará a entidade UserEntity para o Entity Framework.
    public class UserMap : IEntityTypeConfiguration<UserEntity>
    {
        //Implementa o método Configure que será chamado pelo Entity Framework para configurar a entidade UserEntity.
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            //Configura a entidade UserEntity para mapear para a tabela User no banco de dados.
            builder.ToTable("User");
            //Define a propriedade Id como a chave primária da entidade UserEntity.
            builder.HasKey(k => k.Id);
            //Cria um índice na propriedade Email e garante que esse índice é único, ou seja, não podem existir dois usuários com o mesmo email no banco de dados.
            builder.HasIndex(e => e.Email)
                    .IsUnique();
            //Configura a propriedade Name para ser obrigatória (IsRequired) e define um comprimento máximo de 60 caracteres.
            builder.Property(u => u.Name)    
                    .IsRequired()
                    .HasMaxLength(60);
            //Configura a propriedade Email para ter um comprimento máximo de 120 caracteres.
            builder.Property(u => u.Email)
                    .HasMaxLength(120);
        }
    }
}