using Discounting.API.Application.Commands.CreateDiscount;

namespace Discounting.IntegrationTests
{
    public class FakeData
    {
        public static CreateDiscountCommand CreateDiscount()
        {
            return new CreateDiscountCommand()
            {
                Name = "Discount",
                Description = "This is a new Discount",
                ShopId=1
            };
        }
    }
}
