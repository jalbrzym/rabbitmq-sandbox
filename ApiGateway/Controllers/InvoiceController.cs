using MassTransit;
using Messages;
using Messages.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    [Route("api/invoices")]
    public class InvoiceController : Controller
    {
        private readonly IBus _bus;

        public InvoiceController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public IActionResult CreateInvoice(Invoice invoice)
        {
            _bus.Publish(new CreateInvoice
            {
                Invoice = invoice
            });

            return Ok();
        }
    }
}