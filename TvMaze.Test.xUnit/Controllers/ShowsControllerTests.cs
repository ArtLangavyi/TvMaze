using Microsoft.VisualStudio.TestTools.UnitTesting;
using TvMaze.Test.xUnit;
using Xunit.Abstractions;

namespace TvMaze.Controllers.Tests
{
    [TestClass()]
    public class ShowsControllerTests : BaseTest
    {
        public ShowsControllerTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [Fact]
        public async Task GetShowsWithCastAsyncTest()
        {
            var response = await _client.PutAsync("/Shows/overview//1/24".ToUserServiceUrl(), null);

            var responseString = await response.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(responseString);

            response.EnsureSuccessStatusCode();
        }
    }
}