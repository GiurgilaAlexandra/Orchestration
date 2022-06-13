namespace POC.Orchestration.Payment.Server.Handlers
{
    internal class OrderCreatedEventHandler: IConsume <OrderCreatedEvent>
    {
        public void Handle(OrderCreatedEvent orderCreatedEvent)
        {
            DoPayment();
            PublishPaymentCompletedEvent();
        }
       
    }
}
