using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Dtos.Cep
{
    public class CepDtoUpdate
    {
        [Required(ErrorMessage = "Id é campo Obrigatório")]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Cep é um capo obritaório")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Logradouro é um capo obrigatório")]
        public string Logradouro { get; set; }
        
                        public string Numero { get; set; }
        
        [Required(ErrorMessage = "Municipio é um campo obrigatório")]
        public Guid MunicipioId { get; set; }
        
        
    }
}