using Microsoft.EntityFrameworkCore;
using Shopping.Domain.AggregateModel.CategoryAggregate;
using Shopping.Domain.Common;
using Shopping.Infrastructure.Data;

namespace Shopping.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ShopContext _context;

        public CategoryRepository(ShopContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task AddAsync(Category model)
        {
            await _context.Categories.AddAsync(model);
        }

        public async Task<IEnumerable<Category>> GetAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
