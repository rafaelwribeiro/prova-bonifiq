namespace ProvaPub.Repository.Interface;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    IOrderRepository OrderRepository { get; }
    IRandomNumberRepository RandomNumberRepository { get; }
    public IRepository<T> GetRepository<T>() where T : class;
    Task CommitAsync();
}
