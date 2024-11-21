using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Seeds
{
    public static class UfSeeds
    {
        public static void Ufs(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UfEntity>().HasData(
            new UfEntity
            {
                Id = new Guid("1e2302c5-18b3-485a-976b-eac8d061b058"),
                Sigla = "AC", Nome = "Acre",
                CreatedAt = DateTime.UtcNow
            },
            new UfEntity
            {
                Id = new Guid("2f2302c5-18b3-485a-976b-eac8d061b059"), Sigla = "AL", Nome = "Alagoas", CreatedAt = DateTime.UtcNow
            },
            new UfEntity
            {
                Id = new Guid("3a2302c5-18b3-485a-976b-eac8d061b060"), Sigla = "AP", Nome = "Amapá", CreatedAt = DateTime.UtcNow
            },
            new UfEntity
            {
                Id = new Guid("4b2302c5-18b3-485a-976b-eac8d061b061"), Sigla = "AM", Nome = "Amazonas", CreatedAt = DateTime.UtcNow
            },
            new UfEntity
            {
                Id = new Guid("5c2302c5-18b3-485a-976b-eac8d061b062"), Sigla = "BA", Nome = "Bahia", CreatedAt = DateTime.UtcNow
            },
            new UfEntity
            {
                Id = new Guid("6d2302c5-18b3-485a-976b-eac8d061b063"), Sigla = "CE", Nome = "Ceará", CreatedAt = DateTime.UtcNow
            },
            new UfEntity
            {
                Id = new Guid("7e2302c5-18b3-485a-976b-eac8d061b064"), Sigla = "DF", Nome = "Distrito Federal", CreatedAt = DateTime.UtcNow
            },
            new UfEntity
            {
                Id = new Guid("8f2302c5-18b3-485a-976b-eac8d061b065"), Sigla = "ES", Nome = "Espírito Santo", CreatedAt = DateTime.UtcNow
            },
            new UfEntity
            {
                Id = new Guid("9a2302c5-18b3-485a-976b-eac8d061b066"), Sigla = "GO", Nome = "Goiás", CreatedAt = DateTime.UtcNow
            },
            new UfEntity
            {
                Id = new Guid("0b2302c5-18b3-485a-976b-eac8d061b067"), Sigla = "MA", Nome = "Maranhão", CreatedAt = DateTime.UtcNow
            },
            new UfEntity
            {
                Id = new Guid("1c2302c5-18b3-485a-976b-eac8d061b068"), Sigla = "MT", Nome = "Mato Grosso", CreatedAt = DateTime.UtcNow
            },
            new UfEntity
            {
                Id = new Guid("2d2302c5-18b3-485a-976b-eac8d061b069"), Sigla = "MS", Nome = "Mato Grosso do Sul", CreatedAt = DateTime.UtcNow
            },
            new UfEntity
            {
                Id = new Guid("3e2302c5-18b3-485a-976b-eac8d061b070"), Sigla = "MG", Nome = "Minas Gerais", CreatedAt = DateTime.UtcNow
            },
            new UfEntity
            {
                Id = new Guid("4f2302c5-18b3-485a-976b-eac8d061b071"), Sigla = "PA", Nome = "Pará", CreatedAt = DateTime.UtcNow
            },
            new UfEntity
            {
                Id = new Guid("5a2302c5-18b3-485a-976b-eac8d061b072"), Sigla = "PB", Nome = "Paraíba", CreatedAt = DateTime.UtcNow
            },
            new UfEntity
            {
                Id = new Guid("6b2302c5-18b3-485a-976b-eac8d061b073"), Sigla = "PR", Nome = "Paraná", CreatedAt = DateTime.UtcNow
            },
            new UfEntity
            {
                Id = new Guid("7c2302c5-18b3-485a-976b-eac8d061b074"), Sigla = "PE", Nome = "Pernambuco", CreatedAt = DateTime.UtcNow
            },
            new UfEntity
            {
                Id = new Guid("8d2302c5-18b3-485a-976b-eac8d061b075"), Sigla = "PI", Nome = "Piauí", CreatedAt = DateTime.UtcNow
            },
            new UfEntity
            {
                Id = new Guid("9e2302c5-18b3-485a-976b-eac8d061b076"), Sigla = "RJ", Nome = "Rio de Janeiro", CreatedAt = DateTime.UtcNow
            },
            new UfEntity
            {
                Id = new Guid("0f2302c5-18b3-485a-976b-eac8d061b077"), Sigla = "RN", Nome = "Rio Grande do Norte", CreatedAt = DateTime.UtcNow
            },
            new UfEntity
            {
                Id = new Guid("1a2302c5-18b3-485a-976b-eac8d061b078"), Sigla = "RS", Nome = "Rio Grande do Sul", CreatedAt = DateTime.UtcNow
            },
            new UfEntity
            {
                Id = new Guid("2b2302c5-18b3-485a-976b-eac8d061b079"), Sigla = "RO", Nome = "Rondônia", CreatedAt = DateTime.UtcNow
            });
        }
    }
}
