using System;
using System.Threading.Tasks;
using MassTransit;
using Messages;

namespace InvoiceSendingService
{
    public class SendInvoiceConsumer : IConsumer<SendInvoice>
    {
        public Task Consume(ConsumeContext<SendInvoice> context)
        {
            // fetch invoice & send

            Console.WriteLine($"Invoice {context.Message.InvoiceId} has been sent to {context.Message.EmailAddress}");

            return Task.CompletedTask;
        }
    }
}