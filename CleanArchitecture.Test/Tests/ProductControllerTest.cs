using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace CleanArchitecture.Test.Tests
{
    public class ProductControllerTest : TestBase
    {
        private HttpClient ـhttpClient;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IJwtService jwtService;

        public ProductControllerTest(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IJwtService jwtService, WebApplicationFactory<Program> applicationFactory)
        {
            ـhttpClient = applicationFactory.CreateDefaultClient();
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtService = jwtService;
        }

        [Fact]
        public async Task GetAll_ShouldReturnHttpStatusOk()
        {
            var response = await ـhttpClient.GetAsync("api/Products/GetAll");
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetById_ShouldReturnHttpStatusOk(int id)
        {
            var response = await ـhttpClient.GetAsync($"api/Products/GetAll?id={id}");
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("Product_01", "farzamyamini@yahoo.com", "09027159171")]
        [InlineData("Product_02", "farzamyamini@yahoo.com", "09027159171")]
        public async Task Post_ShouldReturnHttpStatusOk(string name, string email, string phone)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Products");
            string token = await AuthenticationHelper.GenerateToken(userManager, signInManager, jwtService);
            request.Headers.Add("Authorization", $"Bearer {token}");

            var model = new ProductCreateDto()
            {
                Id = 0,
                Name = name,
                ManufactureEmail = email,
                ManufacturePhone = phone,
                ProduceDate = DateTime.Now,
                IsAvailable = true
            };

            var strModel = JsonConvert.SerializeObject(model);
            var content = new StringContent(strModel, null, "application/json");
            request.Content = content;
            var response = await ـhttpClient.SendAsync(request);

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(1, "Product_01_Updated", "farzamyamini@yahoo.com", "09027159171")]
        public async Task Put_ShouldReturnHttpStatusOk(int id, string name, string email, string phone)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "api/Products");
            string token = await AuthenticationHelper.GenerateToken(userManager, signInManager, jwtService);
            request.Headers.Add("Authorization", $"Bearer {token}");

            var model = new ProductCreateDto()
            {
                Id = id,
                Name = name,
                ManufactureEmail = email,
                ManufacturePhone = phone,
                ProduceDate = DateTime.Now,
                IsAvailable = true
            };

            var strModel = JsonConvert.SerializeObject(model);
            var content = new StringContent(strModel, null, "application/json");
            request.Content = content;
            var response = await ـhttpClient.SendAsync(request);

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(1)]
        public async Task Delete_ShouldReturnHttpStatusOk(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Products?id={id}");
            string token = await AuthenticationHelper.GenerateToken(userManager, signInManager, jwtService);
            request.Headers.Add("Authorization", $"Bearer {token}");

            var response = await ـhttpClient.SendAsync(request);

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
        }
    }
}
