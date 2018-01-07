using System;
using System.Collections.Generic;

namespace Messages.DTO
{
    public class Invoice
    {
        public DateTime IssueDate { get; set; }

        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string VatId { get; set; }

        public IList<InvoiceLineItem> LineItems { get; set; }

    }
}