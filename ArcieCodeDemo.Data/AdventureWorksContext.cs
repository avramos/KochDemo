using Microsoft.EntityFrameworkCore;

namespace ArcieCodeDemo.Data
{
    public class AdventureWorksContext : DbContext
    {
        public AdventureWorksContext(DbContextOptions<AdventureWorksContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;
    }

    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; } = null!;
        public string ProductNumber { get; set; } = null!;
        public decimal ListPrice { get; set; }
        public DateTime SellStartDate { get; set; }
    }
}