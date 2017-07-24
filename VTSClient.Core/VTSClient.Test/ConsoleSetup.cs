using Autofac;
using VTSClient.Core.Infrastructure.DI;
using VTSClient.DAL.Interfaces;
using VTSClient.DAL.Repositories;

namespace VTSClient.Test
{
    public static class ConsoleSetup
    {
        public static IContainer Container { get; set; }

        public static void Initialize()
        {
            var builder = new ContainerBuilder();

            CoreSetup.Init(builder);

            ConsoleSetup.Container = builder.Build();
        }
    }
}
