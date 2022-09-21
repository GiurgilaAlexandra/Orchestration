using MassTransit;
using Microsoft.Extensions.Hosting;
using POC.Orchestration.Infrastructure;
using POC.Orchestration.Shipping.Server.Consumers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddMassTransit(x =>
        {
            x.ConfigureRabbitmq();
            x.AddConsumer<OrderShipmentStartedEventConsumer>();
        });

    })
    .Build();

host.Run();