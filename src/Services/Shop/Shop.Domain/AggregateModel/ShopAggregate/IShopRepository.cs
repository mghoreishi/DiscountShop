using Shopping.Domain.Common;
using System;


namespace Shopping.Domain.AggregateModel.ShopAggregate
{
    public interface IShopRepository : IRepository<Shop>
    {
        Task<Shop> GetByIdAsync(long id);
        Task AddAsync(Shop model);

        void Update(Shop model);
    }
}
