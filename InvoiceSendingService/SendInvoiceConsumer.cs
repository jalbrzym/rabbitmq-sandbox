using System;
using System.Threading.Tasks;
using MassTransit;
using Messages;

namespace InvoiceSendingService
{
    public class SendInvoiceConsumer : IConsumer<ISendInvoice>
    {
        public SendInvoiceConsumer(IService service)
        {
            
        }

        public Task Consume(ConsumeContext<ISendInvoice> context)
        {
            // fetch invoice & send

            Console.WriteLine($"Invoice {context.Message.InvoiceId} has been sent to {context.Message.EmailAddress}");

            return Task.CompletedTask;
        }
    }
}