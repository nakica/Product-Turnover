using Microsoft.AspNetCore.Mvc;
using ProductTurnover.Infra;

namespace ProductTurnover.Rest.Controllers
{
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ILoggingFacility<ProductController> _log;
        private readonly IProductRepository _productRepo;
        private readonly ITaxation _taxation;

        public ProductController(ILoggingFacility<ProductController> log, IProductRepository productRepo, ITaxation taxation)
        {
            _log = log;
            _productRepo = productRepo;
            _taxation = taxation;
        }

        /// <summary>
        /// Calculate net turnover for a specified product.
        /// </summary>
        [HttpPost("/NetTurnover")]
        public IActionResult NetTurnover(ProductTurnover prodTurnover)
        {
            _log.Trace($"{nameof(NetTurnover)} has been invoked.");
            ActionResult result = null;

            if (string.IsNullOrWhiteSpace(prodTurnover.ProductName))
            {
                _log.Error("Invalid product name.");
                result = BadRequest();
            }
            else if (prodTurnover.EAN?.Length < 8 || prodTurnover.EAN?.Length > 13 || !long.TryParse(prodTurnover?.EAN, out var num))
            {
                _log.Error("Invalid EAN.");
                result = BadRequest();
            }
            else
            {
                var product = _productRepo.Read(prodTurnover.EAN);
                if (product is null)
                {
                    _log.Error($"Product with EAN [{prodTurnover.EAN}] was not found.");
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

            _log.Trace($"{nameof(NetTurnover)} execution completed.");

            return result;
        }
    }
}
