using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.Uf;

namespace Api.Domain.Dtos.Municipio
{
    public class MunicipioDtoCreate
    {
        [Required(ErrorMessage = "Nome de municipio é campo obrigatório")]
        [StringLength(70, ErrorMessage = "Limite de nomeda do municipio é {1} caracteres.")]
        public string Nome { get; set; }
        [Range(0,int.MaxValue,ErrorMessage = "O Código do IBGE Inválido")]
        public int CodIBGE { get; set; }        
        [Required(ErrorMessage = "Código de UF é campo obrigatório")]
        public Guid UfId { get; set; }

    }
}