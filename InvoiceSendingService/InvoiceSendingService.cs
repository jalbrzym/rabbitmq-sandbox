using System;
using MassTransit;
using MassTransit.Util;

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

                sbc.ReceiveEndpoint(host, "poc.invoices", cfg =>
                {
                    cfg.Consumer<SendInvoiceConsumer>();
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