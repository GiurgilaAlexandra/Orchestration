using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using POC.Orchestration.Infrastructure;
using POC.Orchestration.Payment.Server;
using POC.Orchestration.Payment.Server.Handlers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<PaymentService>();
        services.AddMassTransit(x =>
        {
            x.ConfigureRabbitmq();
            x.AddConsumer<OrderCreatedEventConsumer>();
        });

    })
    .Build();

host.Run();