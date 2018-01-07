using System;
using MassTransit;

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

                host.ConnectReceiveEndpoint("poc.invoices", cfg =>
                {
                    cfg.Consumer<CreateInvoiceConsumer>();
                });
            });
        }

        public void Stop()
        {
            _bus.Stop(TimeSpan.FromSeconds(30));
        }
    }
}