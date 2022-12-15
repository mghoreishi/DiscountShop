using Discounting.API.Application.Commands.CreateDiscount;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Discounting.IntegrationTests
{
    public class DiscountingScenarios: DiscountingScenariosBase
    {
        [Fact]
        public async Task CreateDiscount_CheckIfCreated()
        {
            // Arrange
            using Microsoft.AspNetCore.TestHost.TestServer server = CreateServer();
            CreateDiscountCommand content = FakeData.CreateDiscount();

            // Act
            HttpResponseMessage responsePost = await server.CreateClient().PostAsJsonAsync(Post.CreateDiscount(), content);

            // Assert
            responsePost.EnsureSuccessStatusCode();
        }
    }
}