using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.Municipio;

namespace Api.Domain.Interfaces.Services.Município
{
    public interface IMunicipioservice
    {
        Task<MunicipioDto> Get(Guid id);

        Task<MunicipioDtoCompleto> GetCompletoById(Guid Id);

        Task<MunicipioDtoCompleto> GetCompletoByIBGE(int codIBGE);

        Task<IEnumerable<MunicipioDto>> GetAll();

        Task<MunicipioDtoCreateResult> Post(MunicipioDtoCreate municipio);
        Task<MunicipioDtoUpdateResult> Put(MunicipioDtoUpdate municipio);
        Task<bool> Delete(Guid id);
    }
}
