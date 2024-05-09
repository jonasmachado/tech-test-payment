using AutoBogus;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using TechTestPayment.Api.Controllers;
using TechTestPayment.Application.Dto.Http.Request;
using TechTestPayment.Application.Services.Abstractions;

namespace TechTestPayment.Tests.Unit.Layers.Api.Controllers
{
    public class OrderControllerTests
    {
        private readonly OrdersController _controller;

        public OrderControllerTests()
        {
            Mock<IOrderService> orderService = new();
            _controller = new OrdersController(orderService.Object);
        }

        [Fact]
        public async Task Post_WhenCalled_ShouldCreateOrder()
        {
            var request = new AutoFaker<RegisterOrderRequest>().Generate();

            var response = await _controller.Post(request);

            Assert.Equal((int)HttpStatusCode.Created, (response.Result as ObjectResult)?.StatusCode);
        }

        [Fact]
        public async Task Get_WhenCalled_ShouldGetOrder()
        {
            var response = await _controller.Get(id: 1);

            Assert.Equal((int)HttpStatusCode.OK, (response.Result as ObjectResult)?.StatusCode);
        }

        [Fact]
        public async Task Patch_WhenCalled_ShouldUpdateOrder()
        {
            var request = new AutoFaker<UpdateOrderRequest>().Generate();

            var response = await _controller.Patch(request);

            Assert.IsType<OkResult>(response);
        }
    }
}
