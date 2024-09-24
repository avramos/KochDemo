using System.Net.Http.Json;
using ArcieCodeDemo.Data;

namespace ArcieCodeDemo.Web.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetTopProductsAsync(int topN);
    }
}
