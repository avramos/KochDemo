using System.Net.Http.Json;
using ArcieCodeDemo.Data;

namespace ArcieCodeDemo.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductService> _logger;

        public ProductService(HttpClient httpClient, IConfiguration configuration, ILogger<ProductService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<Product>> GetTopProductsAsync(int topN)
        {
            try
            {
                var apiBaseUrl = _configuration["ApiBaseUrl"];
                var response = await _httpClient.GetAsync($"{apiBaseUrl}/api/products/top/{topN}");
                response.EnsureSuccessStatusCode();
                var products = await response.Content.ReadFromJsonAsync<List<Product>>();
                return products ?? new List<Product>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error occurred while calling the API");
                return new List<Product>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred in GetTopProductsAsync");
                return new List<Product>();
            }
        }
    }

}
