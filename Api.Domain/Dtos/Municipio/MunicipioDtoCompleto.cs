using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.Uf;

namespace Api.Domain.Dtos.Municipio
{
    public class MunicipioDtoCompleto
    {
        public Guid Id  { get; set; }

        public string Nome { get; set; }
        
        public int CodIBGE { get; set; }
        
        public Guid UfId { get; set; }

        public UfDto Uf { get; set; }
        
    }
}