using System;
using Topshelf;

namespace InvoiceSendingService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var rc = HostFactory.Run(x =>
            {
                x.Service<InvoiceSendingService>(svc =>
                {
                    svc.ConstructUsing(name => new InvoiceSendingService()); 
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