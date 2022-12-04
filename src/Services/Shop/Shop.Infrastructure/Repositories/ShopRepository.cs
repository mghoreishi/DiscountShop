using Microsoft.EntityFrameworkCore;
using Shopping.Domain.AggregateModel.ShopAggregate;
using Shopping.Domain.Common;
using Shopping.Infrastructure.Data;

namespace Shopping.Infrastructure.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly ShopContext _context;

        public ShopRepository(ShopContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;
        public async Task AddAsync(Shop model)
        {
            await _context.Shops.AddAsync(model);
        }

        public async Task<Shop> GetByIdAsync(long id)
        {
            return await _context.Shops.Where(q => q.Id == id).FirstOrDefaultAsync();
        }

        public void Update(Shop model)
        {
            _context.Entry(model).State = EntityState.Modified;
        }
    }
}
