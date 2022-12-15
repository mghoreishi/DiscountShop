using FluentAssertions;
using Shopping.API.Application.Commands.CreateShop;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Shopping.IntegrationTests
{
    public class ShoppingScenarios: ShoppingScenariosBase
    {
        [Fact]
        public async Task CreateShop_CheckIfCreated()
        {
            // Arrange
            using Microsoft.AspNetCore.TestHost.TestServer server = CreateServer();
            CreateShopCommand content = FakeData.CreateShop();

            // Act
            HttpResponseMessage responsePost = await server.CreateClient().PostAsJsonAsync(Post.CreateShop(), content);

            // Assert
            responsePost.EnsureSuccessStatusCode();
        }
    }
}