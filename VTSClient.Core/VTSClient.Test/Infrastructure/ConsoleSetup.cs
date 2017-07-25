using Autofac;
using VTSClient.Core.Infrastructure.DI;
using VTSClient.DAL.Infrastructure;

namespace VTSClient.Test.Infrastructure
{
    public static class ConsoleSetup
    {
        public static IContainer Container { get; set; }

        public static void Initialize()
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(new DbLocation())
                .As<IDbLocation>();

            builder.RegisterInstance(new ServerUrl())
                .As<IServerUrl>();

            CoreSetup.Init(builder);

            ConsoleSetup.Container = builder.Build();
        }
    }
}
