using System;
using MassTransit;
using MassTransit.Util;
using Shared;

namespace InvoiceSendingService
{
    internal class InvoiceSendingService
    {
        private IBusControl _bus;

        public void Start()
        {
            _bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri(RabbitMqConfig.HostAddress), h =>
                {
                    h.Username(RabbitMqConfig.Username);
                    h.Password(RabbitMqConfig.Password);
                });

                sbc.ReceiveEndpoint(host, RabbitMqConfig.Queues.Invoices, cfg =>
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