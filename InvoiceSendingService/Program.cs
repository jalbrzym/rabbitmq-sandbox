using System;
using Autofac;
using Topshelf;

namespace InvoiceSendingService
{
    public class Program
    {
        public static IContainer Container { get; private set; }

        public static void Main(string[] args)
        {
            Container = AutofacInitializer.Initialize();

            var rc = HostFactory.Run(x =>
            {
                x.Service<InvoiceSendingService>(svc =>
                {
                    svc.ConstructUsing(name => Container.Resolve<InvoiceSendingService>()); 
                    svc.WhenStarted(s => s.Start());
                    svc.WhenStopped(s => s.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Invoice Sending Service");
                x.SetDisplayName("InvoiceSendingService");
                x.SetServiceName("InvoiceSendingService");
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());

            Environment.ExitCode = exitCode;
        }
    }
}