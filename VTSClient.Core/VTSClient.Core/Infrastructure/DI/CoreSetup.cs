using Autofac;
using VTSClient.BLL.Interfaces;
using VTSClient.BLL.Services;
using VTSClient.Core.Infrastructure.Automapper;
using VTSClient.DAL.Interfaces;
using VTSClient.DAL.Repositories;

namespace VTSClient.Core.Infrastructure.DI
{
    public static class CoreSetup
    {
        public static void Init(ContainerBuilder containerBuilder)
        {
            containerBuilder.Register(x => new ApiRepositoryVacation()).As<IApiRepositoryVacation>();

            containerBuilder.Register(x => new SqlRepositoryVacation()).As<ISqlRepositoryVacation>();

            containerBuilder.RegisterType<SqlVacationService>().As<ISqlVacationService>();

            containerBuilder.RegisterType<ApiVacationService>().As<IApiVacationService>();

            AutoMapperCoreConfiguration.Configure();
        }
    }
}
