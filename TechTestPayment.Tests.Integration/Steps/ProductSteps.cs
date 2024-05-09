using AutoBogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using TechTalk.SpecFlow;
using TechTestPayment.Api.Controllers;
using TechTestPayment.Application.Dto.Http.Request;
using TechTestPayment.Application.Dto.Http.Response;
using TechTestPayment.Application.Services.Abstractions;
using TechTestPayment.Domain.Abstractions.UOW;
using TechTestPayment.Tests.Integration.Setup;

namespace TechTestPayment.Tests.Integration.Steps
{
    [Binding]
    public class ProductSteps
    {
        private readonly ProductsController _controller;
        private readonly IUnitOfWork _unitOfWork;

        private RegisterProductRequest? _productRequest;
        private RegisterProductResponse? _productResponse;

        public ProductSteps()
        {
            var serviceProvider = IntegrationTestsSetup.GetServiceProvider();
            _controller = new ProductsController(serviceProvider.GetService<IProductService>()!);
            _unitOfWork = serviceProvider.GetService<IUnitOfWork>()!;
        }

        [Given("que um informo dados válidos para cadastrar um produto")]
        public void GivenIHaveAProductRequest()
        {
            _productRequest = new AutoFaker<RegisterProductRequest>()
                .RuleFor(product => product.Name, faker => faker.Commerce.ProductName())
                .RuleFor(product => product.Price, faker => faker.Random.Decimal(1, 1000))
                .RuleFor(product => product.ItemsRemaining, faker => faker.Random.Int(50, 100))
                .Generate();
        }

        [Given("submeto o post para a rota products")]
        public async Task AndPost()
        {
            var result = (await _controller.Post(_productRequest!) as ObjectResult)!.Value;
            _productResponse = (RegisterProductResponse)result!;
        }

        [Then("o produto é criado com sucesso")]
        public void ThenTheProductIsCreatedSuccessfully()
        {
            _productResponse.ShouldNotBeNull("A response de criação de produto não deve ser nulo");
            _productResponse.Id.ShouldBeGreaterThan(expected: 0);
        }

        [Then("o produto deve constar no banco de dados")]
        public async Task ThenTheSellerMustBeInDatabase()
        {
            var product = (await _unitOfWork.ProductRepository.FindAsync(x => x.Id == _productResponse!.Id)).FirstOrDefault();

            product.ShouldNotBeNull("O vendedor deve existir no banco de dados");
            product.Name.ShouldBe(_productRequest!.Name);
            product.Price.ShouldBe(_productRequest.Price);
        }

        [Given(@"um produto é criado com nome ""(.*)""")]
        public async Task GivenAProductIsCreatedWithName(string name)
        {
            _productRequest = new AutoFaker<RegisterProductRequest>()
                .RuleFor(product => product.Name, name)
                .RuleFor(product => product.Price, faker => faker.Random.Decimal(1, 1000))
                .RuleFor(product => product.ItemsRemaining, faker => faker.Random.Int(50, 100))
                .Generate();

            await _controller.Post(_productRequest);
        }
    }
}
