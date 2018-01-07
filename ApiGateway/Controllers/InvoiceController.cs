using System.Threading.Tasks;
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
        public async Task<IActionResult> CreateInvoice(Invoice invoice)
        {
            await _bus.Publish(new CreateInvoice
            {
                Invoice = invoice
            });

            return Ok();
        }
    }
}