using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;
using TechTestPayment.Domain.Abstractions.UOW;
using TechTestPayment.Tests.Integration.Setup;

namespace TechTestPayment.Tests.Integration.Steps
{
    [Binding]
    public class CleanUpSteps
    {
        [AfterScenario("clean_products")]
        public static async Task CleanProducts()
        {
            var serviceProvider = IntegrationTestsSetup.GetServiceProvider();
            var unitOfWork = serviceProvider.GetService<IUnitOfWork>()!;

            unitOfWork.ProductRepository.FindAsync(x => true).Result.ForEach(x =>
            {
                unitOfWork.SetDeleted(x);
            });

            await unitOfWork.SaveChangesAsync();
        }

        [AfterScenario("clean_sellers")]
        public static async Task CleanSellers()
        {
            var serviceProvider = IntegrationTestsSetup.GetServiceProvider();
            var unitOfWork = serviceProvider.GetService<IUnitOfWork>()!;

            unitOfWork.SellerRepository.FindAsync(x => true).Result.ForEach(x =>
            {
                unitOfWork.SetDeleted(x);
            });

            await unitOfWork.SaveChangesAsync();
        }
    }
}
