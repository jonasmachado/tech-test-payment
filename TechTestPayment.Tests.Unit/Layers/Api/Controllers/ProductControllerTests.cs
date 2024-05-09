using AutoBogus;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using TechTestPayment.Api.Controllers;
using TechTestPayment.Application.Dto.Http.Request;
using TechTestPayment.Application.Services.Abstractions;

namespace TechTestPayment.Tests.Unit.Layers.Api.Controllers
{
    public class ProductControllerTests
    {
        private readonly ProductsController _controller;

        public ProductControllerTests()
        {
            Mock<IProductService> productService = new();
            _controller = new ProductsController(productService.Object);
        }

        [Fact]
        public async Task Post_WhenCalled_ShouldCreateProduct()
        {
            var request = new AutoFaker<RegisterProductRequest>().Generate();

            var response = await _controller.Post(request);

            Assert.Equal((int)HttpStatusCode.Created, (response as ObjectResult)?.StatusCode);
        }
    }
}
