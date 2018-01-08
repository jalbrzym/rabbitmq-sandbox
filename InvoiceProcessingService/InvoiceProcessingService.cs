using System;
using MassTransit;
using MassTransit.Util;

namespace InvoiceProcessingService
{
    internal class InvoiceProcessingService
    {
        private IBusControl _bus;

        public void Start()
        {
            _bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                sbc.ReceiveEndpoint(host, "poc.invoices", cfg =>
                {
                    cfg.Consumer<CreateInvoiceConsumer>();
                });
            });

            TaskUtil.Await(() => _bus.StartAsync());
        }

        public void Stop()
        {
            _bus.Stop(TimeSpan.FromSeconds(30));
        }
    }
}