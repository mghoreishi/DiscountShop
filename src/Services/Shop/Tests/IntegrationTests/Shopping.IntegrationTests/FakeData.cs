using Shopping.API.Application.Commands.CreateShop;


namespace Shopping.IntegrationTests
{
    public class FakeData
    {
        public static CreateShopCommand CreateShop()
        {
            return new CreateShopCommand()
            {
                Name = "Shop",
                Description = "This is a new shop",
                Address = "Berlin, Germany",
                Phone = "4491053766",
                CategoryId = 2
            };
        }
    }
}
