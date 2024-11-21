using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCuting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            // Mapeia UserDto para UserEntity e vice-versa.
            CreateMap<UserDto, UserEntity>()
                .ReverseMap();

            // Mapeia UserDtoCreateResult para UserEntity e vice-versa.
            CreateMap<UserDtoCreateResult, UserEntity>()
                .ReverseMap();

            // Mapeia UserDtoUpdateResult para UserEntity e vice-versa.
            CreateMap<UserDtoUpdateResult, UserEntity>()
                .ReverseMap();
        }
    }
}