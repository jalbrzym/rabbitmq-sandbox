using System;
using MassTransit;
using MassTransit.Util;

namespace InvoiceSendingService
{
    internal class InvoiceSendingService
    {
        private readonly IBusControl _busControl;

        public InvoiceSendingService(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public void Start()
        {
            TaskUtil.Await(() => _busControl.StartAsync());
        }

        public void Stop()
        {
            _busControl.Stop(TimeSpan.FromSeconds(30));
        }
    }
}