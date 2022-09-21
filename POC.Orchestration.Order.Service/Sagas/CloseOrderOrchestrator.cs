using MassTransit;
using POC.Orchestration.Events;

namespace POC.Orchestration.Order.Service.Sagas
{
    public class CloseOrderOrchestrator : IConsumer<OrderShipmentCompletedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public CloseOrderOrchestrator(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<OrderShipmentCompletedEvent> context)
        {
            Console.WriteLine("Shipping completed for order " + context.Message.OrderId);
            await NotifyClient(context.Message.OrderId, "ShippingCompleted");
            await CloseOrder(context.Message.OrderId);
            await NotifyClient(context.Message.OrderId, "Closed");
        }

        private Task CloseOrder(int orderId)
        {
            Console.WriteLine("Closing order " + orderId);
            return Task.CompletedTask;
        }

        private async Task NotifyClient(int orderId, string status)
        {
            await _publishEndpoint.Publish(new ReportingEvent {OrderId = orderId, OrderStatus = status});
        }
    }
}
