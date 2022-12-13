using Xunit;
using FluentAssertions;
using System;
using CSharpFunctionalExtensions;

namespace Shopping.Domain.UnitTest
{
    public class ShopTests
    {
        [Fact]
        public void Constructor_ShouldConstructShopProperly()
        {
            const string name = "maryam shop";
            const string description = "This is maryam shop";
            const string address = "Berlin, Germany";
            const string phone = "09105376648";
            const long categoryId = 1;

            var shopBuilder = new ShopTestBuilder();

            var shop = shopBuilder.Build();


            shop.ShopName.Value.Should().Be(name);
            shop.ShopDescription.Value.Should().Be(description);
            shop.Address.Value.Should().Be(address);
            shop.Phone.Value.Should().Be(phone);
            shop.CategoryId.Should().Be(categoryId);

        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenNameIsNotProvided()
        {
            var shopBuilder = new ShopTestBuilder();

            Action shop = () => shopBuilder.WithName("").Build();

            shop.Should().Throw<ResultFailureException>();
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenDescriptionIsNotProvided()
        {
            var shopBuilder = new ShopTestBuilder();

            Action shop = () => shopBuilder.WithDescription("").Build();

            shop.Should().Throw<ResultFailureException>();
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenAddressIsNotProvided()
        {
            var shopBuilder = new ShopTestBuilder();

            Action shop = () => shopBuilder.WithAddress("").Build();

            shop.Should().Throw<ResultFailureException>();
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenPhoneIsNotProvided()
        {
            var shopBuilder = new ShopTestBuilder();

            Action shop = () => shopBuilder.WithPhone("").Build();

            shop.Should().Throw<ResultFailureException>();
        }
    }
}