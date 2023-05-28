using Microsoft.AspNetCore.Mvc;
using ShopsRU.Data;
using ShopsRU.Logic;
using ShopsRU.Models;

namespace ShopsRU.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private readonly IRepository _repository;

    public HomeController(IRepository repository)
    {
        _repository = repository;
    }

    [HttpPost("[action]")]
    public ActionResult GenerateInvoice([FromBody] Cart cart)
    {
        try
        {
            var checkoutHelper = new CheckoutHelper(_repository);
            var rawInvoice = checkoutHelper.GenerateInvoice(Guid.Parse(cart.UserId), cart.Items);
            return Ok(new 
            {
                FirstName = rawInvoice.InvoiceUser.FirstName,
                LastName = rawInvoice.InvoiceUser.LastName,
                TotalAmount = Math.Round(rawInvoice.TotalAmount, 2),
                TotalDiscount = Math.Round(rawInvoice.TotalDiscount, 2),
                CheckoutAmount = Math.Round(rawInvoice.PayableAmount, 2)
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
