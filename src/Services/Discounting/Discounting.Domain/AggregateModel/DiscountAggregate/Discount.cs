using CSharpFunctionalExtensions;


namespace Discounting.Domain.AggregateModel.DiscountAggregate
{
    public class Discount : EntityBase
    {

        #region - Constructor - 
        public Discount(DiscountName discountName, DiscountDescription discountDescription, long shopId)
        {
            DiscountName = discountName;
            DiscountDescription = discountDescription;
            ShopId = shopId;
        }

        #endregion

        #region - Properties -
        public DiscountName DiscountName { get; private set; }
        public DiscountDescription DiscountDescription { get; private set; }
        public long ShopId { get; private set; }
        #endregion

        #region - Functions -
        #endregion
    }
}
