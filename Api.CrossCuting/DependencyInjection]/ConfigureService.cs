using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCuting.DependencyInjection
{
    public class ConfigureService
    {
       // Método estático para configurar as dependências dos serviços.
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            // Adiciona um serviço transitório para IUserService que será resolvido como UserService.
            // AddScoped indica que uma nova instância do serviço será criada para cada solicitação dentro de um escopo (como uma requisição HTTP).
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<ILoginService, LoginService>();
        }
    }
}