using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class RandomService
	{
		private readonly int seed;
        private readonly TestDbContext _ctx;
		public RandomService(TestDbContext ctx)
        {
            seed = Guid.NewGuid().GetHashCode();

            _ctx = ctx;
        }
        public async Task<int> GetRandom()
        {
            var number = new Random(seed).Next(100);
            await StoreNumber(number);
            return number;
        }

        private async Task StoreNumber(int number)
        {
            if (await _ctx.Numbers.AnyAsync(n => n.Number == number)) return;
            _ctx.Numbers.Add(new RandomNumber() { Number = number });
            _ctx.SaveChanges();
        }
    }
}
