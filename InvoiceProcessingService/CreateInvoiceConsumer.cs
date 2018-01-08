using System;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Messages;
using Messages.DTO;

namespace InvoiceProcessingService
{
    public class CreateInvoiceConsumer : IConsumer<CreateInvoice>
    {
        public async Task Consume(ConsumeContext<CreateInvoice> context)
        {
            var invoice = context.Message.Invoice;

            PrintInvoice(invoice);

            var invoiceId = PersistInvoice(invoice);

            await context.Publish(new SendInvoice
            {
                EmailAddress = "sample.fake@email.xyz",
                InvoiceId = invoiceId
            });
        }

        private Guid PersistInvoice(Invoice invoice)
        {
            // some business logic & persistence
            return Guid.NewGuid();
        }

        private void PrintInvoice(Invoice invoice)
        {
            var sb = new StringBuilder();

            sb.AppendLine("--------------------------------");
            sb.AppendLine($"CompanyName:\t{invoice.CompanyName}");
            sb.AppendLine($"CompanyAddress:\t{invoice.CompanyAddress}");
            sb.AppendLine($"VatId:\t\t{invoice.VatId}");
            sb.AppendLine($"IssueDate:\t{invoice.IssueDate}");
            sb.AppendLine("LineItems:");

            if (invoice.LineItems != null)
            {
                foreach (var lineItem in invoice.LineItems)
                {
                    sb.AppendLine($"\tDescription:\t{lineItem.Description}");
                    sb.AppendLine($"\tQuantity:\t{lineItem.Quantity}");
                    sb.AppendLine($"\tNetValue:\t{lineItem.NetValue}");
                    sb.AppendLine($"\tGrossValue:\t{lineItem.GrossValue}");
                    sb.AppendLine($"\tTaxRate:\t{lineItem.TaxRate}");
                }
            }

            sb.AppendLine("--------------------------------");

            Console.WriteLine(sb.ToString());
        }
    }
}