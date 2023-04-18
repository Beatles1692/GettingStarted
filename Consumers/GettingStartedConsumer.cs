namespace Company.Consumers
{
    using System.Threading.Tasks;
    using MassTransit;
    using Contracts;
    using Microsoft.Extensions.Logging;

    public class GettingStartedConsumer :
        IConsumer<GettingStartedMessage>
    {
        private readonly ILogger<GettingStartedConsumer> _logger;
        public GettingStartedConsumer(ILogger<GettingStartedConsumer> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<GettingStartedMessage> context)
        {
            _logger.LogInformation("Received message: {Value}", context.Message.Value);
            return Task.CompletedTask;
        }
    }
}