﻿using MassTransit;
using POC.Orchestration.Events;

namespace POC.Orchestration.Notification.Server.Consumers
{
    public class ReportingEventConsumer : IConsumer<ReportingEvent>
    {
        public Task Consume(ConsumeContext<ReportingEvent> context)
        {
            Console.WriteLine("Notifying client that the order number " + context.Message.OrderId + " has the status " +
                              context.Message.OrderStatus);
            return Task.CompletedTask;
        }
    }
}
