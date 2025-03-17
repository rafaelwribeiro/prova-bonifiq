using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Repository.Interface;
using ProvaPub.Services.Payments;

namespace ProvaPub.Services
{
    public class OrderService : BaseService<Order>
	{
        private readonly IUnitOfWork _uow;
        private readonly PaymentServiceFactory _paymentServiceFactory;

        public OrderService(IUnitOfWork uow, PaymentServiceFactory paymentServiceFactory) : base(uow)
        {
            _uow = uow;
            _paymentServiceFactory = paymentServiceFactory;
        }

        public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
		{
			var paymentService = _paymentServiceFactory.GetPaymentService(paymentMethod);
			paymentService.Process(customerId, paymentValue);

			return await InsertOrder(new Order() //Retorna o pedido para o controller
            {
                Value = paymentValue
            });


		}

		public async Task<Order> InsertOrder(Order order)
        {
			//Insere pedido no banco de dados
			return (await _uow.GetRepository<Order>().Add(order));
        }
	}
}
