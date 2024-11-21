using System;
using System.Collections.Generic;
using Api.CrossCuting.DependencyInjection;
using Api.CrossCuting.Mappings;
using Api.Data.Context;
using Api.Domain.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Api.Application
{
    // Classe responsável pela configuração inicial da aplicação.
    public class Startup
    {
        // Construtor que recebe as configurações da aplicação.
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _enviroment = env;
            Configuration = configuration;
        }

        // Propriedade que armazena as configurações da aplicação.
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment _enviroment { get; }
        
        

        // Método chamado pelo runtime para adicionar serviços ao contêiner de DI.
        public void ConfigureServices(IServiceCollection services)
        {
            
            if(_enviroment.IsEnvironment("Testing"))
            {
                Environment.SetEnvironmentVariable("DB_CONNECTION", "");
                Environment.SetEnvironmentVariable("DATABASE", "MYSQL");
                Environment.SetEnvironmentVariable("MIGRATION", "APLICAR");
                Environment.SetEnvironmentVariable("Audience", "ExemploAudience");
                Environment.SetEnvironmentVariable("Issuer", "ExemploIssue");
                Environment.SetEnvironmentVariable("Seconds", "43200");
            }
            services.AddControllers();

            // Configura as dependências dos serviços.
            ConfigureService.ConfigureDependenciesService(services);

            // Configura as dependências dos repositórios.
            ConfigureRepository.ConfigureDependenciesRepository(services);

            //Cria uma nova configuração para o AutoMapper, que é uma biblioteca de mapeamento de objetos
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {          
                      //Adiciona um perfil de mapeamento chamado DtoToModelProfile. Perfis são usados no AutoMapper para agrupar configurações de mapeamento relacionadas.
                      cfg.AddProfile(new DtoToModelProfile());
                      cfg.AddProfile(new EntityToDtoProfile());
                      cfg.AddProfile(new ModelToEntityProfile());
            });

            // Esses perfis definem regras e configurações de mapeamento entre diferentes tipos de objetos em sua aplicação, como DTOs (Data Transfer Objects), 
            // entidades de banco de dados e modelos de domínio. Isso facilita a conversão de dados entre diferentes camadas 
            // da aplicação de forma eficiente e centralizada. Interessante, não é? 🚀 Se precisar de mais alguma coisa, só falar!

            IMapper mapper = config.CreateMapper();
            services.AddSingleton (mapper);

            // Cria uma nova instância de SigningConfigurations.
            var signingConfigurations = new Signingconfigurations();

            // Adiciona a instância de SigningConfigurations como um serviço Singleton.
            // Singleton significa que uma única instância do serviço será criada e compartilhada por toda a aplicação.
            services.AddSingleton(signingConfigurations);

            // Cria uma nova instância de TokenConfigurations.
            var tokenConfigurations = new TokenConfigurations();

            // Configura a instância de TokenConfigurations usando as opções da seção "TokenConfigurations" do Configuration.
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations")
            ).Configure(tokenConfigurations);

            //Adiciona serviços de autenticação ao contêiner de serviços e configura as opções de autenticação.
            services.AddAuthentication(authOptions =>
            {
                // Define o esquema de autenticação padrão para JWT Bearer.
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                // Define o esquema de desafio padrão para JWT Bearer.
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                // Obtém os parâmetros de validação do token do objeto bearerOptions.
                var paramsValidation = bearerOptions.TokenValidationParameters;
                // Define a chave de assinatura do emissor dos tokens.
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                // Define o público válido para os tokens (quem pode usá-los).
                paramsValidation.ValidAudience = Environment.GetEnvironmentVariable("Audience");
                // Define o emissor válido dos tokens (quem pode emiti-los).
                paramsValidation.ValidIssuer = Environment.GetEnvironmentVariable("Issuer");

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;
                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Adiciona os serviços de autorização ao contêiner de serviços.
            services.AddAuthorization(auth =>
            {
                // Adiciona uma política de autorização chamada "Bearer".
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    // Adiciona esquemas de autenticação JWT Bearer à política.
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    // Requer que o usuário esteja autenticado para acessar recursos protegidos pela política.
                    .RequireAuthenticatedUser()
                    // Constrói a política de autorização.
                    .Build());
            });


            // Adiciona a instância de TokenConfigurations como um serviço Singleton.
            services.AddSingleton(tokenConfigurations);
            //Instancia Unica

            // Adiciona serviços de controle (API Controllers).
            services.AddControllers();

            // Adiciona e configura o Swagger para gerar a documentação da API.
            services.AddSwaggerGen(c =>
            {
                // Define um documento Swagger com as informações fornecidas.
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1", // Versão da documentação da API.
                    Title = "Curso de API com AspNetCore 3.1 - Na prática", // Título da documentação.
                    Description = "Arquitetura DDD", // Descrição da documentação.
                    TermsOfService = new Uri("http://www.Gymble.com.br"), // URL dos termos de serviço.

                    // Informações de contato.
                    Contact = new OpenApiContact
                    {
                        Name = "Gabriel Silva", // Nome do contato.
                        Email = "Gymble@gmail.com", // Email do contato.
                        Url = new Uri("http://www.Gymble.com.br"), // URL do contato.
                    },

                    // Informações de licença.
                    License = new OpenApiLicense
                    {
                        Name = "Termo de licença de Uso", // Nome da licença.
                        Url = new Uri("http://www.Gymble.com.br") // URL da licença.
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "Entre com o Token JWT",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    }
                );

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },new List<string>()
                        }
                    }    
                );
            });
        }

        // Método chamado pelo runtime para configurar o pipeline de requisições HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configurações específicas para o ambiente de desenvolvimento.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Configurações para ambientes de produção.
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Redireciona requisições HTTP para HTTPS.
            app.UseHttpsRedirection();

            // Habilita a configuração de arquivos estáticos (CSS, JavaScript, etc).
            app.UseStaticFiles();

            // Ativa o middleware do Swagger para gerar a documentação da API.
            app.UseSwagger();

            // Configura a interface do Swagger UI.
            app.UseSwaggerUI(c =>
            {
                // Define o endpoint do Swagger JSON e um título para a documentação.
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Curso de API com AspNetCore 3.1");
                // Define o prefixo de rota da interface do Swagger UI. Quando vazio, a interface será exibida na raiz do aplicativo.
                c.RoutePrefix = string.Empty;
            });

            // Configura o roteamento da aplicação.
            app.UseRouting();

            // Adiciona suporte à autorização.
            app.UseAuthorization();

            // Configura os endpoints de controle.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if(Environment.GetEnvironmentVariable("MIGRATION").ToLower() == "APLICAR".ToLower())
            {
                using (var service = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                        {
                            using (var context = service.ServiceProvider.GetService<MyContext>())
                            {
                                context.Database.Migrate();
                            }  
                        }
            }


        }
    }
}
