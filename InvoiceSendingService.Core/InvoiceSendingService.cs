using System;
using MassTransit;

namespace InvoiceSendingService
{
    internal class InvoiceSendingService
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
                    cfg.Consumer<SendInvoiceConsumer>();
                });
            });
        }

        public void Stop()
        {
            _bus.Stop(TimeSpan.FromSeconds(30));
        }
    }
}