using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Api.Domain.Interfaces.Services.User;
using System.Net;
using Api.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Api.Domain.Dtos.User;

namespace Api.Application.Controllers
{
    //http://localhost:5171/api/users <- Rota do site para acessar a api

    // Define a rota base para o controlador e aplica o atributo ApiController.
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _service;

        public UsersController(IUserService userService)
        {
            _service = userService;
        }
        
        // Define um método HTTP GET para obter todos os usuários.
        [Authorize("Bearer")]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            // Verifica se o estado do modelo é válido.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna um Bad Request (400) se o estado do modelo for inválido.
            }

            try
            {
                // Chama o serviço para obter todos os usuários e retorna um Ok (200) com os dados.
                return Ok(await _service .GetAll());
            }
            catch (ArgumentException e)
            {
                // Retorna um Internal Server Error (500) se ocorrer uma exceção.
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // Define um método HTTP GET para obter todos os usuários separando por Id.
        [Authorize("Bearer")]
        [HttpGet]
        [RouteAttribute("{id}", Name = "GetWithId")]
        public async Task<ActionResult> GetById(Guid id)
        {
            // Verifica se o estado do modelo é válido.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna um Bad Request (400) se o estado do modelo for inválido.
            }

            try
            {
                var result = await _service .Get(id);
                if(result == null)
                {
                    return NotFound();
                }
                // Chama o serviço para obter todos os usuários e retorna um Ok (200) com os dados.
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                // Retorna um Internal Server Error (500) se ocorrer uma exceção.
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDtoCreat user)
        {
            // Verifica se o estado do modelo é válido.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna um Bad Request (400) se o estado do modelo for inválido.
            }

            try
            {
                var result = await _service .Post(user);

                if(result != null)
                {
                    // Created:
                    // Esse método cria uma resposta 201 Created. Ele é usado para indicar que uma nova entidade foi criada com sucesso no servidor.
                    // new Uri(Url.Link("GetWithId", new { id = result.Id })):
                    // Url.Link("GetWithId", new { id = result.Id }): Gera um link de URL para o endpoint nomeado "GetWithId", passando um objeto anônimo { id = result.Id } como parâmetro. Isso ajuda a construir a URL completa para o recurso recém-criado.
                    // new Uri(...): Constrói um novo objeto Uri com o link gerado, que representa a localização do recurso recém-criado.
                    // result:
                    // Esse é o objeto que acabou de ser criado e será retornado como parte do corpo da resposta.
                    return Created(new Uri(Url.Link("GetWithId", new {id = result.Id})),result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (ArgumentException e)
            {
                // Retorna um Internal Server Error (500) se ocorrer uma exceção.
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserDtoUpdate user)
        {
            // Verifica se o estado do modelo é válido.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna um Bad Request (400) se o estado do modelo for inválido.
            }

            try
            {
                var result = await _service .Put(user);

                if(result != null)
                {
                    return Ok (result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (ArgumentException e)
            {
                // Retorna um Internal Server Error (500) se ocorrer uma exceção.
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            // Verifica se o estado do modelo é válido.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna um Bad Request (400) se o estado do modelo for inválido.
            }

            try
            {
                return Ok(await _service .Delete(id));
            }
            catch (ArgumentException e)
            {
                // Retorna um Internal Server Error (500) se ocorrer uma exceção.
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }

}