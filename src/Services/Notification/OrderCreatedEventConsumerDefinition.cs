using BuildingBlocks.Messaging.Events;
using MassTransit;
using RabbitMQ.Client;

namespace Notification;

public class OrderCreatedEventConsumerDefinition : ConsumerDefinition<OrderCreatedEventHandler>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<OrderCreatedEventHandler> consumerConfigurator, IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rabbit)
        {
            rabbit.Bind(nameof(OrderCreatedEvent), x => 
            { 
                x.RoutingKey = "order.create";
                x.ExchangeType = ExchangeType.Topic;
            });
        }
    }
}
