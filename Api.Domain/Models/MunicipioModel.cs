using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Models
{
    public class MunicipioModel
    {
        private String _nome;
        public String Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        private int _codIBGE;
        public int CodIBGE
        {
            get { return _codIBGE; }
            set { _codIBGE = value; }
        }

        private Guid _ufId;
        public Guid UfId
        {
            get { return _ufId; }
            set { _ufId = value; }
        }
          
    }
}