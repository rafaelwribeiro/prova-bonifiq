using ProvaPub.Models;
using ProvaPub.Repository.Interface;

namespace ProvaPub.Services
{
	public class ProductService : BaseService<Product>
    {
        private readonly IUnitOfWork _uow;

        public ProductService(IUnitOfWork uow) : base(uow)
        {
            _uow = uow;
        }
	}
}
