using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Repository.Interface;

namespace ProvaPub.Services
{
    public class CustomerService : BaseService<Customer>
    {
        private readonly IUnitOfWork _uow;


        public CustomerService(IUnitOfWork uow) : base(uow)
        {
            _uow = uow;
        }

        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
        {
            if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));

            if (purchaseValue <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseValue));

            var customerRepo = _uow.GetRepository<Customer>();
            var orderRepo = _uow.GetRepository<Order>();

            //Business Rule: Non registered Customers cannot purchase
            var customer = await customerRepo.GetByFilter(c => c.Id == customerId);
            if (customer == null) throw new InvalidOperationException($"Customer Id {customerId} does not exists");

            //Business Rule: A customer can purchase only a single time per month
            var baseDate = DateTime.UtcNow.AddMonths(-1);
            var ordersInThisMonth = await orderRepo.CountAsync(s => s.CustomerId == customerId && s.OrderDate >= baseDate);
            if (ordersInThisMonth > 0)
                return false;

            //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
            var haveBoughtBefore = await customerRepo.CountAsync(s => s.Id == customerId && s.Orders.Any());
            if (haveBoughtBefore == 0 && purchaseValue > 100)
                return false;

            //Business Rule: A customer can purchases only during business hours and working days
            if (DateTime.UtcNow.Hour < 8 || DateTime.UtcNow.Hour > 18 || DateTime.UtcNow.DayOfWeek == DayOfWeek.Saturday || DateTime.UtcNow.DayOfWeek == DayOfWeek.Sunday)
                return false;


            return true;
        }

    }
}
