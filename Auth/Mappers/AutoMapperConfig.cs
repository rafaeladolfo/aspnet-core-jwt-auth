using AutoMapper;

namespace Auth.Mappers
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                EntityToModelMapping.ApplyMapping(cfg);
            });
        }
    }
}
