using POC.Orchestration.Order.Service.Models;

namespace POC.Orchestration.Order.Service.Sagas
{
    public class CreateOrderSaga : IConsume<PaymentCompletedEvent>
    {
        public Create(CreateOrderModel createOrderModel)
        {
            var order = CreateNewOrder();
            SaveOrder(order);
            PublishOrderCreated(order);
        }
        
        public Handle(PaymentCompletedEvent paymentCompletedEvent)
        {
            UpdateOrder(paymentCompletedEvent);
            PublishOrderPaidEvent(paymentCompletedEvent);
        }
    }
}
