using Discounting.Domain.AggregateModel.DiscountAggregate;
using Discounting.Domain.Common;
using Discounting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Discounting.Infrastructure.Repositories
{
    public class DiscountRepository: IDiscountRepository
    {
        private readonly DiscountContext _context;

        public DiscountRepository(DiscountContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;
        public async Task AddAsync(Discount model)
        {
            await _context.Discounts.AddAsync(model);
        }

        public async Task<Discount> GetByIdAsync(long id)
        {
            return await _context.Discounts.Where(q => q.Id == id).FirstOrDefaultAsync();
        }

        public void Update(Discount model)
        {
            _context.Entry(model).State = EntityState.Modified;
        }
    }
}

