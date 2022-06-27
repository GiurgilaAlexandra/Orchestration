using MassTransit;
using Microsoft.Extensions.Hosting;
using POC.Orchestration.Events;

namespace POC.Orchestration.Payment.Server
{
    internal class PaymentService : IHostedService
    {
        private readonly IPublishEndpoint publishEndpoint;

        public PaymentService(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await publishEndpoint.Publish<OrderCreatedEvent>(new { OrderId = 7 }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
