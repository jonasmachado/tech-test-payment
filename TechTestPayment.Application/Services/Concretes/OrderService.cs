using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using TechTestPayment.Application.Dto.Http.Request;
using TechTestPayment.Application.Dto.Http.Response;
using TechTestPayment.Application.Services.Abstractions;
using TechTestPayment.Cross.Enums;
using TechTestPayment.Cross.Exceptions;
using TechTestPayment.Domain.Abstractions.UOW;
using TechTestPayment.Domain.Entities;

namespace TechTestPayment.Application.Services.Concretes
{
    public class OrderService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<OrderService> logger,
        Domain.Abstractions.Services.IProductService productService,
        IValidator<RegisterOrderRequest> registerOrderValidator,
        IValidator<UpdateOrderRequest> updateOrderValidator) : BaseService, IOrderService
    {
        public async Task<GetOrderResponse> Get(int id)
        {
            logger.LogInformation("Retrieving order with id: {Id}", id);

            var order = await unitOfWork.OrderRepository.Get(id);

            return mapper.Map<GetOrderResponse>(order);
        }

        public async Task<RegisterOrderResponse> Register(RegisterOrderRequest request)
        {
            logger.LogInformation("Order registration requested");

            ValidateRequest(registerOrderValidator, request);

            await using var transaction = await unitOfWork.BeginTransactionAsync();

            try
            {
                await unitOfWork.SellerRepository.ThrowIfNotExistsAsync(x => x.Id == request.SellerId, ErrorCodes.SellerDoesNotExist);

                var productIds = request.ProductsItems!.Select(y => y.Id).ToList();
                var orderItemsMap = request.ProductsItems!.ToDictionary(item => item.Id, item => item.Amount);

                await productService.PerformStockReduction(productIds, orderItemsMap);

                var order = mapper.Map<Order>(request);

                await unitOfWork.OrderRepository.InsertAsync(order);
                await unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();

                logger.LogInformation("Ordered registered with id: {Id}", order.Id);

                return new RegisterOrderResponse { Id = order.Id };
            }
            catch (TechTestPaymentException ex)
            {
                logger.LogError("A known error was thrown: {Message}", ex.Message);
                await transaction.RollbackAsync();
                throw;
            }
            catch (DbException ex)
            {
                logger.LogError("A db exception was thrown: {Message}", ex.Message);
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateStatus(UpdateOrderRequest request)
        {
            logger.LogInformation("Requested order status update with id {Id} to status {Status}", request.Id, request.Status);

            ValidateRequest(updateOrderValidator, request);

            var order = (await unitOfWork.OrderRepository.FindAsync(x => x.Id == request.Id))
                    .FirstOrDefault() ?? throw new DatabaseException(ErrorCodes.OrderNotFound);

            order.UpdateStatus(request.Status);

            await unitOfWork.SaveChangesAsync();

            logger.LogInformation("Order status updated successfully");
        }
    }
}
