using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductTurnover.Rest.Controllers
{
    [ApiController]
    public class ProductController : Controller
    {
        [HttpPost]
        public IActionResult Post(ProductTurnover turnover)
        {
            ActionResult result = null;

            if (string.IsNullOrWhiteSpace(turnover.ProductName))
            {
                result = BadRequest();
            }
            else if (turnover.EAN < 8 || turnover.EAN > 13)
            {
                result = BadRequest();
            }
            else if (false)
            {
                result = BadRequest();
            }
            else
            {
                result = Ok(new ProductTurnoverBreakdown());
            }

            return result;
        }
    }
}
