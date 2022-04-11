using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ProductTurnover.Infra;
using System;

namespace ProductTurnover.Rest.Controllers
{
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ILoggingFacility<ProductController> _log;
        private readonly IProductTurnoverCache _cache;
        private readonly IProductRepository _productRepo;
        private readonly ITaxation _taxation;

        public ProductController(ILoggingFacility<ProductController> log, IProductTurnoverCache cache, IProductRepository productRepo, ITaxation taxation)
        {
            _log = log;
            _cache = cache;
            _productRepo = productRepo;
            _taxation = taxation;
        }

        /// <summary>
        /// Calculate net turnover for a specified product.
        /// </summary>
        [HttpPost("/NetTurnover")]
        public IActionResult NetTurnover(ProductTurnover productTurnover)
        {
            _log.Trace($"{nameof(NetTurnover)} has been invoked.");
            ActionResult result = null;

            if (string.IsNullOrWhiteSpace(productTurnover?.ProductName))
            {
                _log.Error("Invalid product name.");
                result = BadRequest();
            }
            else if (productTurnover.EAN?.Length < 8 || productTurnover.EAN?.Length > 13 || !long.TryParse(productTurnover?.EAN, out var num))
            {
                _log.Error("Invalid EAN.");
                result = BadRequest();
            }
            else if (_cache.TryGetValue(productTurnover.EAN, out ProductTurnoverBreakdown turnover))
            {
                result = Ok(turnover);
            }
            else
            {
                var product = _productRepo.Read(productTurnover.EAN);
                if (product is null)
                {
                    _log.Error($"Product with EAN [{productTurnover.EAN}] was not found.");
                    result = NotFound();
                }
                else
                {
                    var netTurnover = _taxation.CalculateNetTurnover(productTurnover.GrossTurnover, product.Category.VAT);

                    var turnoverBreakdown = new ProductTurnoverBreakdown
                    {
                        GrossTurnover = productTurnover.GrossTurnover,
                        NetTurnover = netTurnover,
                        VAT = product.Category.VAT
                    };

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                    _cache.Add(turnoverBreakdown, productTurnover.EAN);

                    result = Ok(turnoverBreakdown);
                }
            }

            _log.Trace($"{nameof(NetTurnover)} execution completed.");

            return result;
        }
    }
}
