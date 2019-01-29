using Auth.Core.DTO;
using AutoMapper;

namespace Auth.Mappers
{
    internal static class EntityToModelMapping
    {
        public static void ApplyMapping(IMapperConfigurationExpression cfg)
        {           
            cfg.CreateMap<Entities.User, UserDTO>(); 
            cfg.CreateMap<UserDTO, Entities.User>();
        }
    }
}
