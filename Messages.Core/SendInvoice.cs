using System;

namespace Messages
{
    public class SendInvoice
    {
        public Guid InvoiceId { get; set; }
        public string EmailAddress { get; set; }
    }
}