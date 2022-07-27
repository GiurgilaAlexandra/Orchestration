using MassTransit;
using POC.Orchestration.Events;
using POC.Orchestration.Order.Service.Models;

namespace POC.Orchestration.Order.Service.Sagas
{
    public class CreateOrderSaga
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateOrderSaga(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public async Task Create(CreateOrderModel createOrderModel)
        {
            var order = CreateNewOrder();
            await SaveOrder(order);
            await PublishOrderCreated(order);
        }

        private async Task PublishOrderCreated(OrderModel order)
        {
            await _publishEndpoint.Publish(new OrderCreatedEvent { OrderId = order.Id });
        }

        private Task<OrderModel> SaveOrder(OrderModel order)
        {
            order.Id = 8;
            return Task.FromResult(order);
        }

        private OrderModel CreateNewOrder()
        {
            return new OrderModel();
        }
    }
}
