using MassTransit;
using POC.Orchestration.Events;
using POC.Orchestration.Order.Service.Models;

namespace POC.Orchestration.Order.Service.Sagas
{
    public class CreateOrderOrchestrator
    {
        private readonly IRequestClient<PaymentRequest> _paymentClient;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateOrderOrchestrator(
            IRequestClient<PaymentRequest> paymentClient,
            IPublishEndpoint publishEndpoint)
        {
            _paymentClient = paymentClient;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Create(CreateOrderModel createOrderModel)
        {
            var order = CreateNewOrder(createOrderModel);
            await SaveOrder(order);
            Console.WriteLine("Created order number: " + order.Id);
            await NotifyClient(order.Id, "Created");
            
            var paymentResponse = await SendPaymentRequest(order);
            await NotifyClient(order.Id, "PaymentRequested");
            if (!paymentResponse.IsSucceeded)
            {
                throw new Exception("Payment failed!");
            }

            await NotifyClient(order.Id, "PaymentCompleted");

            await PublishOrderShipmentStartedEvent(order);
            await NotifyClient(order.Id, "ShippingStarted");
        }

        private async Task<PaymentResult> SendPaymentRequest(OrderModel order)
        {
            Console.WriteLine("Requesting payment for order " + order.Id);
            var response = await _paymentClient.GetResponse<PaymentResult>(new PaymentRequest {OrderId = order.Id});
            Console.WriteLine("Payment success for order " + order.Id + " is: " + response.Message.IsSucceeded);
            return response.Message;
        }

        private async Task PublishOrderShipmentStartedEvent(OrderModel order)
        {
            Console.WriteLine("Requesting shipping for order " + order.Id);
            await _publishEndpoint.Publish(new OrderShipmentStartedEvent { OrderId = order.Id });
        }

        private async Task NotifyClient(int orderId, string status)
        {
            await _publishEndpoint.Publish(new ReportingEvent {OrderId = orderId, OrderStatus = status});
        }

        private Task<OrderModel> SaveOrder(OrderModel order)
        {
            order.Id = Random.Shared.Next(10);
            return Task.FromResult(order);
        }

        private OrderModel CreateNewOrder(CreateOrderModel createOrderModel)
        {
            return new OrderModel();
        }
    }
}
