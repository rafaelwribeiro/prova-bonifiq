using ProvaPub.Models;
using ProvaPub.Repository.Interface;

namespace ProvaPub.Repository;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(TestDbContext db) : base(db)
    {
    }
}
