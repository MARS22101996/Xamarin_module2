using AutoMapper;
using VTSClient.Core.Infrastructure.Automapper.Profiles;

namespace VTSClient.Core.Infrastructure.Automapper
{
    public static class AutoMapperCoreConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new DtoToEntityProfile());
                cfg.AddProfile(new EntityToDtoProfile());
            });
        }
    }
}
