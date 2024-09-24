using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcieCodeDemo.Data
{
    public interface IProductRepository
    {
        Task<List<Product>> GetTopProductsAsync(int topN);
    }
}
