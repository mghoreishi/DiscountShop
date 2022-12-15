using Shopping.Domain.AggregateModel.ShopAggregate;


namespace Shopping.Domain.UnitTest
{
    public class ShopTestBuilder
    {
        private string _name = "maryam shop";
        private string _description = "This is maryam shop";
        private string _address = "Berlin, Germany";
        private string _phone = "09105376648";
        private const long _categoryId = 1;


        public ShopTestBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public ShopTestBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public ShopTestBuilder WithAddress(string address)
        {
            _address = address;
            return this;
        }

        public ShopTestBuilder WithPhone(string phone)
        {
            _phone = phone;
            return this;
        }

        public Shop Build()
        {
            return new Shop(
            shopName: ShopName.Create(_name).Value,
            shopDescription: ShopDescription.Create(_description).Value,
            address: Address.Create(_address).Value,
            phone: Phone.Create(_phone).Value,
            categoryId: _categoryId
            );
        }
    }
}
