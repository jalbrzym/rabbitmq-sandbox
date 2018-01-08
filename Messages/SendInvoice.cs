using System;

namespace Messages
{
    public interface ISendInvoice
    {
        Guid InvoiceId { get; set; }
        string EmailAddress { get; set; }
    }

    public class SendInvoice : ISendInvoice
    {
        public Guid InvoiceId { get; set; }
        public string EmailAddress { get; set; }
    }
}