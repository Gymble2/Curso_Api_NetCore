using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    // Define um repositório genérico base para operações CRUD.
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        // Referência ao contexto do banco de dados.
        protected readonly MyContext _context;

        // Conjunto de entidades do tipo T no contexto do banco de dados.
        private DbSet<T> _dbSet;

        // Construtor que inicializa o contexto e o DbSet.
        public BaseRepository(MyContext context)
        {
            // Atribui o valor do parâmetro context (que é uma instância de MyContext) à variável _context da classe.
            //Isso permite que a instância do repositório use o contexto do banco de dados para realizar operações como consultas e 
            //salvamentos. Basicamente, conecta o repositório ao banco de dados.
            _context = context;

            //Inicializa a variável _dbSet chamando o método Set<T>() no contexto (_context). Isso retorna um DbSet<T>, que é uma coleção de todas as entidades do tipo T no contexto.
            //Isso permite que o repositório tenha acesso direto à coleção de entidades do tipo T no banco de dados.
            _dbSet = _context.Set<T>();
        }

        // Método assíncrono para deletar uma entidade pelo ID (ainda não implementado).
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                // Usa SingleOrDefaultAsync para buscar uma entidade no DbSet cuja propriedade Id corresponde ao valor id passado como argumento.
                // Se encontrar a entidade, result conterá essa entidade. Se não encontrar, result será null.
                var result = await _dbSet.SingleOrDefaultAsync(x => x.Id == id);

                //Verifica se result é null. Se for, significa que a entidade não foi encontrada no banco 
                //de dados e retorna false, indicando que a operação de exclusão falhou.
                if(result == null)
                {
                    return false;
                }

                // Se a entidade foi encontrada (result não é null), remove a entidade do DbSet.
                _dbSet.Remove(result);

                //Salva as alterações no banco de dados de forma assíncrona. Isso efetivamente persiste a remoção da entidade no banco de dados.
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Método assíncrono para inserir uma nova entidade.
        public async Task<T> InsertAsync(T Item)
        {
            try
            {
                // Se o ID da entidade for vazio, gera um novo GUID.
                if (Item.Id == Guid.Empty)
                {
                    Item.Id = Guid.NewGuid();
                }

                // Define a data de criação para a entidade.
                Item.CreatedAt = DateTime.UtcNow;

                // Adiciona a nova entidade ao DbSet.
                _dbSet.Add(Item);

                // Salva as alterações no banco de dados.
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Lança a exceção se algo der errado.
                throw ex;
            }

            // Retorna a entidade inserida.
            return Item;
        }

        public async Task<bool> ExistAsync (Guid id){
            //Verifica se existe
            return await _dbSet.AnyAsync(x => x.Id.Equals (id));
        }

        // Método assíncrono para selecionar uma entidade pelo ID (ainda não implementado).
        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                //Usa o método SingleOrDefaultAsync para buscar uma entidade no _dbSet cujo Id corresponde ao id fornecido.
                //O SingleOrDefaultAsync é um método do Entity Framework que faz uma consulta ao banco de dados para encontrar uma única entidade 
                //que atenda a uma condição especificada
                return await _dbSet.SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Método assíncrono para selecionar todas as entidades (ainda não implementado).
        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
             return await _dbSet.ToListAsync ();   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Método assíncrono para atualizar uma entidade (ainda não implementado).
        public async Task<T> UpdateAsync(T Item)
        {
            try
            {   
                 //Usa o método SingleOrDefaultAsync para buscar uma entidade no DbSet que tenha o mesmo Id do item passado. Se encontrar, result conterá a entidade; se não, será null.
                var result = await _dbSet.SingleOrDefaultAsync(x => x.Id == Item.Id);
                //Verifica se a entidade result é null. Se for, retorna null, indicando que a entidade não foi encontrada.
                if(result == null)
                {
                    return null;
                }

                //Atualiza o campo UpdateAt do item para a data e hora atuais (UtcNow). Mantém o campo CreatedAt do item igual ao 
                //CreatedAt da entidade existente, garantindo que a data de criação original não seja alterada.
                Item.UpdateAt = DateTime.UtcNow; 
                Item.CreatedAt =  result.CreatedAt;

                //Atualiza a entidade existente (result) no contexto com os valores do item passado. Isso copia todos os valores do item para result.
                _context.Entry(result).CurrentValues.SetValues(Item);
                await _context.SaveChangesAsync();
                //Salva as mudanças feitas no contexto no banco de dados de forma assíncrona.
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Item;
        }
    }
}
