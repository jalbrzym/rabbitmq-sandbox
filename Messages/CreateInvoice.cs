using Messages.DTO;

namespace Messages
{
    public interface ICreateInvoice
    {
        Invoice Invoice { get; set; }
    }

    public class CreateInvoice : ICreateInvoice
    {
        public Invoice Invoice { get; set; }
    }
}