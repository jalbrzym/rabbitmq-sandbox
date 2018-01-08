namespace Messages.DTO
{
    public class InvoiceLineItem
    {
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal NetValue { get; set; }
        public decimal GrossValue { get; set; }
        public decimal TaxRate { get; set; }
    }
}