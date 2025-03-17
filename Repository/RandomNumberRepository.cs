using ProvaPub.Models;
using ProvaPub.Repository.Interface;

namespace ProvaPub.Repository;

public class RandomNumberRepository : Repository<RandomNumber>, IRandomNumberRepository
{
    public RandomNumberRepository(TestDbContext db) : base(db)
    {
    }
}
