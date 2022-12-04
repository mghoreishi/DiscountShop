using Shop.Domain.Common;


namespace Shop.Domain.AggregateModel.CategoryAggregate
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetAsync();
        Task AddAsync(Category model);
    }
}
