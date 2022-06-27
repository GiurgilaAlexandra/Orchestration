using MassTransit;
using POC.Orchestration.Events;

namespace POC.Orchestration.Payment.Server.Handlers
{
    internal class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        private async Task PublishPaymentCompletedEvent()
        {

        }

        private async Task DoPayment()
        {

        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            Console.WriteLine(context.Message.OrderId);
            await DoPayment();
            await PublishPaymentCompletedEvent();
        }
    }
}
