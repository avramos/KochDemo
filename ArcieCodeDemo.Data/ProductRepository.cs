using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArcieCodeDemo.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly AdventureWorksContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(AdventureWorksContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Product>> GetTopProductsAsync(int topN)
        {
            try
            {
                _logger.LogInformation($"Executing stored procedure GetTopProducts with parameter @TopN = {topN}");

                // Line below executes the GetTopProducts stored procedure
                var result = await _context.Products
                    .FromSqlRaw($"EXEC [dbo].[GetTopProducts] @TopN = {topN}")
                    .ToListAsync();
                _logger.LogInformation($"Stored procedure executed successfully. Returned {result.Count} products.");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while executing GetTopProducts stored procedure");
                throw;
            }
        }
    }
}
