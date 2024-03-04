using Auth.Registration.Api.Dtos;
using Auth.Sqlite.Entities;
using AutoMapper;

namespace Auth.Registration.Api.Mappers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterDto, AccountEntity>();
        }
    }
}
