using Discounting.Domain.AggregateModel.DiscountAggregate;


namespace Discounting.Domain.UnitTest
{
    public class DiscountFactory
    {
        public static Discount Create()
        {
            return new(
            discountName: DiscountName.Create("Discount").Value,
            discountDescription: DiscountDescription.Create("Description").Value,
            shopId: 1
            );
        }


        public static Discount CreateWithName(string name)
        {
            return new(
             discountName: DiscountName.Create(name).Value,
             discountDescription: DiscountDescription.Create("Description").Value,
             shopId: 1
             );
        }

        public static Discount CreateWithDescription(string description)
        {
            return new(
             discountName: DiscountName.Create("Discount").Value,
             discountDescription: DiscountDescription.Create(description).Value,
             shopId: 1
             );
        }
    }
}
