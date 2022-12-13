using CSharpFunctionalExtensions;
using FluentAssertions;
using System;
using Xunit;

namespace Discounting.Domain.UnitTest
{
    public class DiscountTests
    {
        [Fact]
        public void Constructor_Should_Construct_Discount_Properly()
        {
            //Arrange
            const string name = "Discount";
            const string description = "Description";

            //Act
            var discount = DiscountFactory.Create();

            //Assert
            discount.DiscountName.Value.Should().Be(name);
            discount.DiscountDescription.Value.Should().Be(description);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenNameIsNotProvided()
        {
            Action discount = () => DiscountFactory.CreateWithName("");

            discount.Should().Throw<ResultFailureException>();
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenDescriptionIsNotProvided()
        {
            Action discount = () => DiscountFactory.CreateWithDescription("");

            discount.Should().Throw<ResultFailureException>();
        }
    }
}