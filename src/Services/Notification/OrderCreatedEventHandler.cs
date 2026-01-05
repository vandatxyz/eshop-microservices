using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Notification;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger)
    : IConsumer<OrderCreatedEvent>
{
    public Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {DomainEvent}", context.Message.GetType().Name);
        var order = context.Message.Order;
        logger.LogInformation("Sending email to {Email} for Order {OrderId}", order.ShippingAddress.EmailAddress, order.Id);
        return Task.CompletedTask;
    }
}
