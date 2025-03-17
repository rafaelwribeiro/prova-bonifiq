using ProvaPub.Models;
using ProvaPub.Repository.Interface;

namespace ProvaPub.Repository;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(TestDbContext db) : base(db)
    {
    }
}
