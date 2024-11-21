using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces
{
    /*
     Explicação assincronismo
     O assincronismo, ou programação assíncrona, permite que o código execute operações 
     sem bloquear o fluxo principal do programa. Em vez de esperar uma operação terminar, 
     como uma solicitação de rede, o programa continua a executar outras tarefas. Uma vez 
     que a operação assíncrona termina, um "callback" ou "promessa" é invocada para lidar 
     com o resultado. Isso é ótimo para melhorar a eficiência e a capacidade de resposta, 
     especialmente em aplicações de rede e I/O intensivo
    */
    
    public interface IRepository<T> where T : BaseEntity
    {
        // Insere uma nova entidade de forma assíncrona.
        Task<T> InsertAsync(T Item);

        // Atualiza uma entidade existente de forma assíncrona.
        Task<T> UpdateAsync(T Item);

        // Deleta uma entidade com base no ID fornecido de forma assíncrona.
        Task<bool> DeleteAsync(Guid id);

        // Seleciona uma entidade com base no ID fornecido de forma assíncrona.
        Task<T> SelectAsync(Guid id);

        // Seleciona todas as entidades de forma assíncrona.
        Task<IEnumerable<T>> SelectAsync();

        Task<bool> ExistAsync (Guid id);
    }
}
