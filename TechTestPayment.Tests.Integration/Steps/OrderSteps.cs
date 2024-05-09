using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using TechTalk.SpecFlow;
using TechTestPayment.Api.Controllers;
using TechTestPayment.Application.Dto.Http.Request;
using TechTestPayment.Application.Dto.Http.Response;
using TechTestPayment.Cross.Exceptions;
using TechTestPayment.Domain.Abstractions.UOW;
using TechTestPayment.Domain.Enums;
using TechTestPayment.Tests.Integration.Setup;

namespace TechTestPayment.Tests.Integration.Steps
{
    [Binding]
    public class OrderSteps
    {
        private readonly OrdersController _controller;
        private readonly IUnitOfWork _unitOfWork;

        private RegisterOrderRequest? _orderRequest;
        private RegisterOrderResponse? _orderResponse;

        private int _transitionOrderId;
        private OrderStatus _transitionStatus;

        public OrderSteps()
        {
            var serviceProvider = IntegrationTestsSetup.GetServiceProvider();
            _controller = serviceProvider.GetService<OrdersController>()!;
            _unitOfWork = serviceProvider.GetService<IUnitOfWork>()!;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _orderRequest = new RegisterOrderRequest() { ProductsItems = [] };
        }

        [Given(@"que adiciono um produto com o nome ""(.*)"" com quantidade ""(.*)""")]
        public async Task GivenThatIAddAProductWithName(string productName, int quantidade)
        {
            var product = (await _unitOfWork.ProductRepository.FindAsync(x => x.Name == productName)).FirstOrDefault();
            var id = product?.Id ?? 0;

            _orderRequest!.ProductsItems!.Add(new RegisterOrderRequest.ProductItem
            {
                Id = id,
                Amount = quantidade
            });
        }

        [Given("que informo o vendedor com o nome \"(.*)\"")]
        public async Task GivenThatIAddASellerWithName(string sellerName)
        {
            var seller = (await _unitOfWork.SellerRepository.FindAsync(x => x.Name == sellerName)).FirstOrDefault();
            var id = seller?.Id ?? 0;

            _orderRequest!.SellerId = id;
        }

        [Given("submeto o post para a rota orders")]
        public async Task AndPost()
        {
            var actionResult = await _controller.Post(_orderRequest!);
            _orderResponse = ((actionResult.Result as ObjectResult)!.Value as RegisterOrderResponse)!;
        }

        [Then("o pedido é criado com sucesso")]
        public void ThenTheOrderIsCreatedSuccessfully()
        {
            _orderResponse.ShouldNotBeNull("A response de criação de pedido não deve ser nulo");
            _orderResponse.Id.ShouldBeGreaterThan(expected: 0);
        }

        [Then("o pedido deve constar no banco de dados")]
        public void ThenTheOrderMustBeInDatabase()
        {
            var order = _unitOfWork.OrderRepository.Find(x => x.Id == _orderResponse!.Id).FirstOrDefault();

            order.ShouldNotBeNull("O pedido deve existir no banco de dados");
            order.SellerId.ShouldBe(_orderRequest!.SellerId);
            order.OrderProducts.Count.ShouldBe(_orderRequest.ProductsItems!.Count);
        }

        [Then("ao submeter deve lançar um erro com a mensagem \"(.*)\"")]
        public async Task SubmitMustThrowWithMessage(string message)
        {
            var exception = await Assert.ThrowsAsync<DatabaseException>(() => _controller.Post(_orderRequest!));
            exception.Message.ShouldBe(message);
        }

        [Given("que crio um pedido novo")]
        public async Task GivenThatICreateANewOrder()
        {
            var existingSeller = (await _unitOfWork.SellerRepository.FindAsync(x => x.Id > 0)).FirstOrDefault();
            var existigProduct = (await _unitOfWork.ProductRepository.FindAsync(x => x.Id > 0)).FirstOrDefault();

            existigProduct.ShouldNotBeNull("Ao menos um produto deve existir no banco de dados");
            existingSeller.ShouldNotBeNull("Ao menos um vendedor deve existir no banco de dados");

            _orderRequest = new RegisterOrderRequest()
            {
                SellerId = existingSeller.Id,
                ProductsItems = [new RegisterOrderRequest.ProductItem {
                    Id = existigProduct.Id,
                    Amount = 10
                }]
            };

            var actionResult = await _controller.Post(_orderRequest);
            var response = ((actionResult.Result as ObjectResult)!.Value as RegisterOrderResponse)!;

            _transitionOrderId = response.Id;
        }

        [Given(@"atualizo o status do pedido para ""(.*)""")]
        public async Task GivenThatIUpdateTheOrderStatusTo(string status)
        {
            var request = new UpdateOrderRequest { Id = _transitionOrderId, Status = Enum.Parse<OrderStatus>(status) };
            await _controller.Patch(request);
        }

        [Then("o status do pedido deve ser \"(.*)\"")]
        public void ThenTheOrderStatusShouldBe(string status)
        {
            var order = _unitOfWork.OrderRepository.Find(x => x.Id == _transitionOrderId).FirstOrDefault();
            order!.Status.ShouldBe(Enum.Parse<OrderStatus>(status));
        }

        [Given(@"informo o status do pedido para ""(.*)""")]
        public void GivenThatIInformTheOrderStatusTo(string status)
        {
            _transitionStatus = Enum.Parse<OrderStatus>(status);
        }

        [When("submeto a atualização do status do pedido deve lançar erro \"(.*)\"")]
        public async Task WhenISubmitTheOrderStatusUpdateShouldThrow(string erro)
        {
            var request = new UpdateOrderRequest { Id = _transitionOrderId, Status = _transitionStatus };
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _controller.Patch(request));

            exception.Message.ShouldBe(erro);
        }

        [When(@"o pedido deve constar na base com o status ""(.*)""")]
        [Then(@"o pedido deve constar na base com o status ""(.*)""")]
        public void ThenTheOrderMustBeInDatabaseWithStatus(string status)
        {
            var order = _unitOfWork.OrderRepository.Find(x => x.Id == _transitionOrderId).FirstOrDefault();
            order!.Status.ShouldBe(Enum.Parse<OrderStatus>(status));
        }
    }
}
