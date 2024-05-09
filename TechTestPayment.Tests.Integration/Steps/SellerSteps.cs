using AutoBogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using TechTalk.SpecFlow;
using TechTestPayment.Api.Controllers;
using TechTestPayment.Application.Dto.Http.Request;
using TechTestPayment.Application.Dto.Http.Response;
using TechTestPayment.Domain.Abstractions.UOW;
using TechTestPayment.Tests.Integration.Setup;

namespace TechTestPayment.Tests.Integration.Steps
{
    [Binding]
    public class SellerSteps
    {
        private readonly SellersController _controller;
        private readonly IUnitOfWork _unitOfWork;

        private RegisterSellerRequest? _sellerRequest;
        private RegisterSellerResponse? _sellerResponse;

        public SellerSteps()
        {
            var serviceProvider = IntegrationTestsSetup.GetServiceProvider();
            _controller = serviceProvider.GetService<SellersController>()!;
            _unitOfWork = serviceProvider.GetService<IUnitOfWork>()!;
        }

        [Given("que um informo dados válidos para cadastrar um vendedor")]
        public void GivenThatSellerDataIsInformedAndValid()
        {
            _sellerRequest = new AutoFaker<RegisterSellerRequest>()
                .RuleFor(seller => seller.Name, f => f.Name.FullName())
                .RuleFor(seller => seller.Email, f => f.Internet.Email())
                .RuleFor(seller => seller.Cpf, f => f.Random.Replace("###########"))
                .RuleFor(seller => seller.Phone, f => f.Random.Replace("##9########"))
                .Generate();
        }

        [Given("submeto o post para a rota sellers")]
        public async Task AndPost()
        {
            var result = (await _controller.Post(_sellerRequest!) as ObjectResult)!.Value;
            _sellerResponse = (RegisterSellerResponse)result!;
        }

        [Then("o vendedor é criado com sucesso")]
        public void ThenTheSellerIsCreatedSuccessfully()
        {
            _sellerResponse.ShouldNotBeNull("A response de criação de vendedor não deve ser nulo");
            _sellerResponse.Id.ShouldBeGreaterThan(expected: 0);
        }

        [Then("o vendedor deve constar no banco de dados")]
        public async Task ThenTheSellerMustBeInDatabase()
        {
            var seller = (await _unitOfWork.SellerRepository.FindAsync(x => x.Id == _sellerResponse!.Id)).FirstOrDefault();

            seller.ShouldNotBeNull("O vendedor deve existir no banco de dados");
            seller.Cpf.ShouldBe(_sellerRequest!.Cpf);
            seller.Email.ShouldBe(_sellerRequest.Email);
            seller.Name.ShouldBe(_sellerRequest.Name);
            seller.Phone.ShouldBe(_sellerRequest.Phone);
        }

        [Given(@"que um vendedor é criado com nome ""(.*)""")]
        public async Task GivenThatASellerIsCreatedWithName(string name)
        {
            _sellerRequest = new AutoFaker<RegisterSellerRequest>()
                .RuleFor(seller => seller.Name, _ => name)
                .RuleFor(seller => seller.Email, f => f.Internet.Email())
                .RuleFor(seller => seller.Cpf, f => f.Random.Replace("###########"))
                .RuleFor(seller => seller.Phone, f => f.Random.Replace("##9########"))
                .Generate();

            await _controller.Post(_sellerRequest);
        }
    }
}
