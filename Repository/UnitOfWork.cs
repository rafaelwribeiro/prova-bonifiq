using ProvaPub.Repository.Interface;

namespace ProvaPub.Repository;

public class UnitOfWork : IUnitOfWork
{
    private ICustomerRepository? _customerRepo;
    private IOrderRepository? _orderRepo;
    private IProductRepository? _productRepo;
    private IRandomNumberRepository? _randomNumberRepo;

    private readonly Dictionary<Type, object> _repositories = new();
    private readonly TestDbContext _ctx;

    public UnitOfWork(TestDbContext ctx)
    {
        _ctx = ctx;
    }

    public ICustomerRepository CustomerRepository
    {
        get
        {
            return _customerRepo = _customerRepo ?? new CustomerRepository(_ctx);
        }
    }

    public IOrderRepository OrderRepository
    {
        get
        {
            return _orderRepo = _orderRepo ?? new OrderRepository(_ctx);
        }
    }

    public IProductRepository ProductRepository
    {
        get
        {
            return _productRepo = _productRepo ?? new ProductRepository(_ctx);
        }
    }

    public IRandomNumberRepository RandomNumberRepository
    {
        get
        {
            return _randomNumberRepo = _randomNumberRepo ?? new RandomNumberRepository(_ctx);
        }
    }

    public IRepository<T> GetRepository<T>() where T : class
    {
        if (_repositories.TryGetValue(typeof(T), out var repo))
        {
            return (IRepository<T>)repo;
        }

        var repository = new Repository<T>(_ctx);
        _repositories[typeof(T)] = repository;
        return repository;
    }

    public async Task CommitAsync()
    {
        await _ctx.SaveChangesAsync();
    }
}
