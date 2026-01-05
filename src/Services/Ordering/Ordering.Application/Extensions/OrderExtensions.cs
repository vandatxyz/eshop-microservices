namespace Ordering.Application.Extensions;
public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
    {
        return orders.Select(order => new OrderDto(
            Id: order.Id.Value,
            CustomerId: order.CustomerId.Value,
            OrderName: order.OrderName.Value,
            ShippingAddress: new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress!, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.ZipCode),
            BillingAddress: new AddressDto(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress!, order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.State, order.BillingAddress.ZipCode),
            Payment: new PaymentDto(order.Payment.CardName!, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.CVV, order.Payment.PaymentMethod),
            Status: order.Status,
            OrderItems: order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()
        ));
    }

    public static OrderDto ToOrderDto(this Order order)
    {
        return DtoFromOrder(order);
    }

    public static BuildingBlocks.Messaging.Events.OrderCreatedEvent ToOrderCreatedEvent(this Order order)
    {
        var orderItems = order.OrderItems.Select(oi => new BuildingBlocks.Messaging.Events.OrderItemEventDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList();
        
        var shippingAddress = new BuildingBlocks.Messaging.Events.AddressEventDto(
            order.ShippingAddress.FirstName, 
            order.ShippingAddress.LastName, 
            order.ShippingAddress.EmailAddress!, 
            order.ShippingAddress.AddressLine, 
            order.ShippingAddress.Country, 
            order.ShippingAddress.State, 
            order.ShippingAddress.ZipCode);

        var billingAddress = new BuildingBlocks.Messaging.Events.AddressEventDto(
            order.BillingAddress.FirstName, 
            order.BillingAddress.LastName, 
            order.BillingAddress.EmailAddress!, 
            order.BillingAddress.AddressLine, 
            order.BillingAddress.Country, 
            order.BillingAddress.State, 
            order.BillingAddress.ZipCode);

        var payment = new BuildingBlocks.Messaging.Events.PaymentEventDto(
            order.Payment.CardName!, 
            order.Payment.CardNumber, 
            order.Payment.Expiration, 
            order.Payment.CVV, 
            order.Payment.PaymentMethod);

        var status = (BuildingBlocks.Messaging.Events.OrderStatus)(int)order.Status;

        var orderDto = new BuildingBlocks.Messaging.Events.OrderEventDto(
            Id: order.Id.Value,
            CustomerId: order.CustomerId.Value,
            OrderName: order.OrderName.Value,
            ShippingAddress: shippingAddress,
            BillingAddress: billingAddress,
            Payment: payment,
            Status: status,
            OrderItems: orderItems
        );

        return new BuildingBlocks.Messaging.Events.OrderCreatedEvent(orderDto);
    }

    private static OrderDto DtoFromOrder(Order order)
    {
        return new OrderDto(
                    Id: order.Id.Value,
                    CustomerId: order.CustomerId.Value,
                    OrderName: order.OrderName.Value,
                    ShippingAddress: new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress!, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.ZipCode),
                    BillingAddress: new AddressDto(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress!, order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.State, order.BillingAddress.ZipCode),
                    Payment: new PaymentDto(order.Payment.CardName!, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.CVV, order.Payment.PaymentMethod),
                    Status: order.Status,
                    OrderItems: order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()
                );
    }
}
