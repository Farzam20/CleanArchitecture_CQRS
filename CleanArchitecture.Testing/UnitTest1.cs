using Microsoft.AspNetCore.Mvc.Testing;

namespace CleanArchitecture.Testing
{
    [TestClass]
    public class UnitTest1
    {
        private HttpClient _httpClient;

        public UnitTest1()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }

        [TestMethod]
        public async Task TestMethod1()
        {
            var response = await _httpClient.GetAsync("api/Products/GetAll");
            var stringResult = await response.Content.ReadAsStringAsync();

            //var response = await httpClient.GetAsync("api/Products/GetAll");
            Assert.AreEqual(1, 1);
        }
    }
}