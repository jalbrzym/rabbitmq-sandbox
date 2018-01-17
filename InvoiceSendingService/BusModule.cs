using System;
using Autofac;
using MassTransit;
using Shared;

namespace InvoiceSendingService
{
    public class BusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();

                var busControl = Bus.Factory.CreateUsingRabbitMq(sbc =>
                {
                    var host = sbc.Host(new Uri(RabbitMqConfig.HostAddress), h =>
                    {
                        h.Username(RabbitMqConfig.Username);
                        h.Password(RabbitMqConfig.Password);
                    });

                    sbc.ReceiveEndpoint(host, RabbitMqConfig.Queues.Invoices, cfg =>
                    {
                        cfg.Consumer<SendInvoiceConsumer>(context, consumer =>
                        {
                        });
                    });
                });

                return busControl;
            })
            .As<IBusControl>()
            .SingleInstance()
            .AutoActivate();
        }
    }
}