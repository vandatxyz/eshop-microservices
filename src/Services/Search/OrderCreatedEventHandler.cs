using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Search;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger)
    : IConsumer<OrderCreatedEvent>
{
    public Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {DomainEvent}", context.Message.GetType().Name);
        var order = context.Message.Order;
        logger.LogInformation("Indexing Order {OrderId} to Elasticsearch...", order.Id);
        // Logic to save to ElasticSearch would go here
        return Task.CompletedTask;
    }
}
