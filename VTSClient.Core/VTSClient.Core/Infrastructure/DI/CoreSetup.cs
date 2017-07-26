using System;
using System.Collections.Generic;
using Autofac;
using AutoMapper;
using VTSClient.BLL.Interfaces;
using VTSClient.BLL.Services;
using VTSClient.Core.Infrastructure.Automapper;
using VTSClient.Core.Infrastructure.Automapper.Profiles;
using VTSClient.DAL.Interfaces;
using VTSClient.DAL.Repositories;

namespace VTSClient.Core.Infrastructure.DI
{
    public static class CoreSetup
    {
        public static void Init(ContainerBuilder containerBuilder)
        {
            containerBuilder.Register(
                    c => new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile(new DtoToEntityProfile());
                        cfg.AddProfile(new EntityToDtoProfile());
                    }))
                .AsSelf()
                .SingleInstance();

            containerBuilder.Register(
                    c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterType<SqlVacationService>().As<ISqlVacationService>();

            containerBuilder.RegisterType<ApiVacationService>().As<IApiVacationService>();

            containerBuilder.RegisterGeneric(typeof(CommonApiRepository<>))
                .As(typeof(IApiRepository<>))
                .InstancePerDependency();

            containerBuilder.RegisterGeneric(typeof(CommonSqlRepository<>))
                .As(typeof(ISqlRepository<>))
                .InstancePerDependency();

            AutoMapperCoreConfiguration.Configure();
        }
    }
}
