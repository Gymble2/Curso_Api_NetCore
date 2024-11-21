using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;
using Api.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCuting.DependencyInjection
{
    // Define uma classe para configurar os repositórios.
    public class ConfigureRepository
    {
        // Método estático para configurar as dependências do repositório.
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            // Adiciona um serviço de escopo para IRepository<> que será resolvido como BaseRepository<>.
            //AddScoped indica que uma nova instância do serviço será criada para cada solicitação dentro de um escopo (como uma requisição HTTP).
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUSerRepository, UserImplementations>();
            
            if(Environment.GetEnvironmentVariable("DATABASE").ToLower() == "SQLSERVER".ToLower())
            {
                var connectionStringSqlServer = Environment.GetEnvironmentVariable("DB_CONNECTION");

                serviceCollection.AddDbContext<MyContext>(
                    options => options.UseSqlServer (connectionStringSqlServer)
                );
            }

            if(Environment.GetEnvironmentVariable("DATABASE").ToLower() == "MYSQL".ToLower())
            {
                serviceCollection.AddDbContext<MyContext>(
                    options => options.UseMySql (Environment.GetEnvironmentVariable("DB_CONNECTION"))
                );
            }
            
        }
    }
}