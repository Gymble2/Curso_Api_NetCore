using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services.User
{
    public interface IUserService
    {
        Task<UserDto> Get (Guid id);

        Task<IEnumerable<UserDto>> GetAll ();

        Task<UserDtoCreateResult> Post (UserDtoCreat user);

        Task<UserDtoUpdateResult> Put (UserDtoUpdate user);

        Task<bool> Delete (Guid id);
    }
}