using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;
using Api.Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    // Classe que implementa o serviço de login, utilizando várias dependências
    {
        // Declaração dos campos privados para as dependências.
        private IUSerRepository _repository;
        private Signingconfigurations _signingconfigurations;
    
        private IConfiguration _configuration { get; }

        // Construtor que inicializa os campos com as dependências injetadas.
        public LoginService(
            IUSerRepository repository,
            Signingconfigurations signingconfigurations,
            IConfiguration configuration
        )
        {
            _repository = repository; // Repositório de usuários
            _signingconfigurations = signingconfigurations; // Configurações de assinatura
            _configuration = configuration; // Configurações da aplicação
        }

        // Método assíncrono que tenta autenticar um usuário pelo login.
        public async Task<object> FindByLogin(LoginDto user)
        {
            var baseUser = new UserEntity();

            // Verifica se o usuário é válido e se o email não é nulo ou vazio.
            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                // Busca o usuário pelo email no repositório.
                baseUser = await _repository.FindByLogin(user.Email);

                // Se não encontrar o usuário, retorna um objeto indicando falha na autenticação.
                if (baseUser == null)
                {
                    return new
                    {
                        authenticated = false,
                        messsage = "Falha ao autenticar"
                    };
                }
                else
                {
                    // Cria a identidade de usuário com base no email.
                    var identity = new ClaimsIdentity(
                        new GenericIdentity(user.Email),
                        new[]
                        {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // jti: ID do token
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Email), // Nome único do usuário
                        }
                    );

                    DateTime createDate = DateTime.Now; // Data de criação do token
                    DateTime expirationDate = createDate + TimeSpan.FromSeconds(Convert.ToInt32(Environment.GetEnvironmentVariable("Seconds"))); // Data de expiração do token

                    var handler = new JwtSecurityTokenHandler(); // Manipulador de tokens JWT
                    string token = CreateToken(identity, createDate, expirationDate, handler); // Criação do token

                    // Retorna um objeto de sucesso com o token e detalhes do usuário.
                    return SuccessObject(createDate, expirationDate, token, baseUser);
                }
            }
            else
            {
                 return new
                    {
                        authenticated = false,
                        messsage = "Falha ao autenticar"
                    };
            }
        }

        // Método privado que cria o token JWT.
        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = Environment.GetEnvironmentVariable("Issuer"), // Emissor do token
                Audience = Environment.GetEnvironmentVariable("Audience"), // Público do token
                SigningCredentials = _signingconfigurations.signingCredentials, // Credenciais de assinatura
                Subject = identity, // Identidade do usuário
                NotBefore = createDate, // Data de validade inicial
                Expires = expirationDate, // Data de expiração
            });

            var token = handler.WriteToken(securityToken); // Escreve o token em formato JWT
            return token; // Retorna o token
        }

        // Método privado que cria o objeto de sucesso com detalhes do token e do usuário.
        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, UserEntity user)
        {
            return new
            {
                authenticated = true, // Indica que a autenticação foi bem-sucedida
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"), // Data de criação do token
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"), // Data de expiração do token
                acessToken = token, // Token de acesso
                userName = user.Email, // Email do usuário
                name = user.Name, // Nome do usuário
                
                message = "Usuário Logado com sucesso" // Mensagem de sucesso
            };
        }
    }

}