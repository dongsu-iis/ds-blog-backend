using Api.Dtos;
using AutoMapper;
using Type = Core.Entities.Type;

namespace Api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Type, TypeDto>().ReverseMap();
        }
    }
}
