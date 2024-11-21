using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Models
{
    public class CepModel
    {
        private string _cep;
        public string Cep
        {
            get { return _cep; }
            set { _cep = value; }
        }
        
        private string _logadouro;
        public string Logadouro
        {
            get { return _logadouro; }
            set { _logadouro = value; }
        }
        
        private string _numero;
        public string Numero
        {
            get { return _numero; }
            set { _numero = string.IsNullOrEmpty(value) ? "S/N" : value; }
        }
        
        private Guid _municipioId;
        public Guid MunicipioId
        {
            get { return _municipioId; }
            set { _municipioId = value; }
        }
        
    }
}