using ProvaPub.Models;
using ProvaPub.Repository.Interface;

namespace ProvaPub.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(TestDbContext db) : base(db)
    {
    }
}
