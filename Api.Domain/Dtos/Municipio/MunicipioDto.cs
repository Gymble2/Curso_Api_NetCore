using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Dtos.Municipio
{
    public class MunicipioDto
    {
        public Guid Id  { get; set; }

        public string Nome { get; set; }
        
        public int CodIBGE { get; set; }
        
        public Guid UfId { get; set; }
        
        
    }
}