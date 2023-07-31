using CleanArchitecture.Application.CQRS;
using CleanArchitecture.Application.CQRS.ProductFiles.Commands;
using CleanArchitecture.Application.CQRS.ProductFiles.Handlers;
using CleanArchitecture.Application.CQRS.ProductFiles.Queries;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Services.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Moq;
using System.Numerics;

namespace CleanArchitecture.Test.Tests
{
    public class ProductControllerTest
    {
        private readonly Mock<IProductService> _productServiceMock;

        public ProductControllerTest()
        {
            _productServiceMock = new();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("cd9f51f0-a61a-4398-961a-8aad983db84c")]
        public async Task GetAll_Handle_ShouldReturn_ListProductDisplayDto(string? createdByUserId)
        {
            // Arrange
            var query = new GetAllProductsQuery(createdByUserId);
            var handler = new GetAllProductsQueryHandler(_productServiceMock.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.True(result.Status);
        }

        [Theory]
        [InlineData(1, "Product_01", "f_yamini72@yahoo.com", "09027159171", "cd9f51f0-a61a-4398-961a-8aad983db84c")]
        [InlineData(2, "Product_02", "farzamyamini@yahoo.com", "09215488280", "cd9f51f0-a61a-4398-961a-8aad983db84c")]
        public async Task GetById_Handle_ShouldReturn_ProductDisplayDto(int id, string title, string email, string phone, string currentUserId)
        {
            // Arrange
            var query = new GetProductQuery(id);
            var handler = new GetProductQueryHandler(_productServiceMock.Object);

            _productServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<CancellationToken>(), It.IsAny<int>()))
                .ReturnsAsync(new Product()
                {
                    Id = id,
                    CreatedByUserId = currentUserId,
                    Name = title,
                    ManufactureEmail = email,
                    ManufacturePhone = phone,
                    ProduceDate = DateTime.Now,
                    IsAvailable = true
                });

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.True(result.Status);
        }

        [Theory]
        [InlineData(1, "Product_01", "f_yamini72@yahoo.com", "09027159171", "cd9f51f0-a61a-4398-961a-8aad983db84c")]
        public async Task Post_Handle_ShouldReturn_ProductDisplayDto(int id, string title, string email, string phone, string currentUserId)
        {
            // Arrange
            var query = new CreateProductCommand(new ProductCreateDto()
            {
                Id = id,
                Name = title,
                ManufactureEmail = email,
                ManufacturePhone = phone,
                ProduceDate = DateTime.Now,
                IsAvailable = true
            }, currentUserId);
            var handler = new CreateProductCommandHandler(_productServiceMock.Object);

            _productServiceMock.Setup(x => x.ProductIsUnique(It.IsAny<Product>())).ReturnsAsync(true);

            _productServiceMock.Setup(x => x.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>(), It.IsAny<bool>()))
                .ReturnsAsync(new Product()
                {
                    Id = id,
                    CreatedByUserId = currentUserId,
                    Name = title,
                    ManufactureEmail = email,
                    ManufacturePhone = phone,
                    ProduceDate = DateTime.Now,
                    IsAvailable = true
                });

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.True(result.Status);
        }

        [Theory]
        [InlineData(1, "Product_01", "f_yamini72@yahoo.com", "09027159171", "cd9f51f0-a61a-4398-961a-8aad983db84c")]
        public async Task Put_Handle_ShouldReturn_ProductDisplayDto(int id, string title, string email, string phone, string currentUserId)
        {
            // Arrange
            var query = new UpdateProductCommand(new ProductCreateDto()
            {
                Id = id,
                Name = title,
                ManufactureEmail = email,
                ManufacturePhone = phone,
                ProduceDate = DateTime.Now,
                IsAvailable = true
            }, currentUserId);
            var handler = new UpdateProductCommandHandler(_productServiceMock.Object);

            _productServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<CancellationToken>(), It.IsAny<int>()))
                .ReturnsAsync(new Product()
                {
                    Id = id,
                    CreatedByUserId = currentUserId,
                    Name = title,
                    ManufactureEmail = email,
                    ManufacturePhone = phone,
                    ProduceDate = DateTime.Now,
                    IsAvailable = true
                });

            _productServiceMock.Setup(x=>x.ProductIsUnique(It.IsAny<Product>())).ReturnsAsync(true);

            _productServiceMock.Setup(x => x.UpdateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>(), It.IsAny<bool>()))
                .ReturnsAsync(new Product()
                {
                    Id = id,
                    CreatedByUserId = currentUserId,
                    Name = title,
                    ManufactureEmail = email,
                    ManufacturePhone = phone,
                    ProduceDate = DateTime.Now,
                    IsAvailable = true
                });

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.True(result.Status);
        }

        [Theory]
        [InlineData(1, "cd9f51f0-a61a-4398-961a-8aad983db84c")]
        public async Task Delete_Handle_ShouldReturn_ProductDisplayDto(int id, string currentUserId)
        {
            // Arrange
            var query = new DeleteProductCommand(id, currentUserId);
            var handler = new DeleteProductCommandHandler(_productServiceMock.Object);

            _productServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<CancellationToken>(), It.IsAny<int>()))
                .ReturnsAsync(new Product()
                {
                    Id = id,
                    CreatedByUserId = currentUserId,
                    Name = "Product_01",
                    ManufactureEmail = "f_yamini72@yahoo.com",
                    ManufacturePhone = "09027159171",
                    ProduceDate = DateTime.Now,
                    IsAvailable = true
                });

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.True(result.Status);
        }
    }
}
