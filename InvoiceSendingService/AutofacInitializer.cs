using Autofac;

namespace InvoiceSendingService
{
    public static class AutofacInitializer
    {
        public static IContainer Initialize()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<BusModule>();

            containerBuilder.RegisterType<InvoiceSendingService>();
            containerBuilder.RegisterType<SampleService>().As<ISampleService>();

            return containerBuilder.Build();
        }
    }
}