using Microsoft.AspNetCore.Mvc;
using ProductTurnover.Infra;

namespace ProductTurnover.Rest.Controllers
{
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly ITaxation _taxation;

        public ProductController(IProductRepository productRepo, ITaxation taxation)
        {
            _productRepo = productRepo;
            _taxation = taxation;
        }

        [HttpPost("/NetTurnover")]
        public IActionResult NetTurnover(ProductTurnover prodTurnover)
        {
            ActionResult result = null;

            if (string.IsNullOrWhiteSpace(prodTurnover.ProductName))
            {
                result = BadRequest();
            }
            else if (prodTurnover.EAN?.Length < 8 || prodTurnover.EAN?.Length > 13 || !long.TryParse(prodTurnover?.EAN, out var num))
            {
                result = BadRequest();
            }
            else
            {
                var product = _productRepo.Read(prodTurnover.EAN);
                if (product is null)
                {
                    result = NotFound();
                }
                else
                {
                    var netTurnover = _taxation.CalculateNetTurnover(prodTurnover.GrossTurnover, product.Category.VAT);

                    var turnoverBreakdown = new ProductTurnoverBreakdown
                    {
                        GrossTurnover = prodTurnover.GrossTurnover,
                        NetTurnover = netTurnover,
                        VAT = product.Category.VAT
                    };

                    result = Ok(turnoverBreakdown);
                }
            }

            return result;
        }
    }
}
