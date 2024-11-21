using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
    //Cria as entidades que vão ser aplicadas na tabela
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        private DateTime? _createAt;
        public DateTime? CreatedAt
        {
            get { return _createAt; }
            set { _createAt = (value == null? DateTime.UtcNow : value); }
        }
        public DateTime? UpdateAt { get; set; }   

        
    }
}