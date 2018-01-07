using System;
using Topshelf;

namespace InvoiceProcessingService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var rc = HostFactory.Run(x =>
            {
                x.Service<InvoiceProcessingService>(svc =>
                {
                    svc.ConstructUsing(name => new InvoiceProcessingService());
                    svc.WhenStarted(s => s.Start());
                    svc.WhenStopped(s => s.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Invoice Processing Service");
                x.SetDisplayName("InvoiceProcessingService");
                x.SetServiceName("InvoiceProcessingService");
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());

            Environment.ExitCode = exitCode;
        }
    }
}