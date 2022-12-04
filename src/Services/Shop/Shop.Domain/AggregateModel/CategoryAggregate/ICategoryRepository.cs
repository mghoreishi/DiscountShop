using Shopping.Domain.Common;


namespace Shopping.Domain.AggregateModel.CategoryAggregate
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetAsync();
        Task AddAsync(Category model);
    }
}
