using System;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GettingStarted;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IBusControl _bus;

    public Worker(ILogger<Worker> logger, IBusControl bus)
    {
        _logger = logger;
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _bus.StartAsync(stoppingToken);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await _bus.Publish(new GettingStartedMessage { Value = "Hello World" }, stoppingToken);
                await Task.Delay(1000, stoppingToken);
            }
        }
        finally
        {
            await _bus.StopAsync(stoppingToken);
        }
    }
}