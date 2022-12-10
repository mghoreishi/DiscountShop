using Discounting.Domain.Common;


namespace Discounting.Domain.AggregateModel.DiscountAggregate
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        Task<Discount> GetByIdAsync(long id);
        Task AddAsync(Discount model);

    
    }
}
