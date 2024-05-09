using AutoBogus;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using TechTestPayment.Api.Controllers;
using TechTestPayment.Application.Dto.Http.Request;
using TechTestPayment.Application.Services.Abstractions;

namespace TechTestPayment.Tests.Unit.Layers.Api.Controllers
{
    public class SellerControllerTests
    {
        private readonly SellersController _controller;

        public SellerControllerTests()
        {
            Mock<ISellerService> sellerService = new();
            _controller = new SellersController(sellerService.Object);
        }

        [Fact]
        public async Task Post_WhenCalled_ShouldCreateSeller()
        {
            var request = new AutoFaker<RegisterSellerRequest>().Generate();

            var response = await _controller.Post(request);

            Assert.Equal((int)HttpStatusCode.Created, (response as ObjectResult)?.StatusCode);
        }
    }
}
