using ArcieCodeDemo.Web.Services;
using Microsoft.AspNetCore.Mvc;
using ArcieCodeDemo.Data;

namespace ArcieCodeDemo.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int topN = 10)
        {
            try
            {
                var products = await _productService.GetTopProductsAsync(topN);
                if (products == null || !products.Any())
                {
                    _logger.LogWarning("No products were returned from the service");
                    return View(new List<Product>()); // Return an empty list instead of null
                }
                return View(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching top products");
                return View("Error");
            }
        }
    }
}