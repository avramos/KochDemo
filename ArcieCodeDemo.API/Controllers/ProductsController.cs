using ArcieCodeDemo.Data;
using Microsoft.AspNetCore.Mvc;

namespace ArcieCodeDemo.API.Controllers
{
    [ApiController] // This attribute (decoration) indicates that this class is an API controller
    [Route("api/[controller]")] // This attribute defines the base route for this controller
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductRepository productRepository, ILogger<ProductsController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet("top/{topN}")] // This attribute defines the HTTP method and route for this action
        public async Task<ActionResult<IEnumerable<Product>>> GetTopProducts(int topN)
        {
            try
            {
                _logger.LogInformation($"Attempting to fetch top {topN} products");
                var products = await _productRepository.GetTopProductsAsync(topN);
                _logger.LogInformation($"Fetched {products?.Count ?? 0} products");

                if (products == null || !products.Any())
                {
                    _logger.LogWarning("No products found");
                    return NotFound("No products found");
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Error handling: Log the error and return a 500 status code
                _logger.LogError(ex, "Error occurred while fetching top products");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}