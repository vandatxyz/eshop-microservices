namespace BuildingBlocks.Messaging.Events;

public record OrderCreatedEvent(OrderEventDto Order) : IntegrationEvent;

public record OrderEventDto(
    Guid Id,
    Guid CustomerId,
    string OrderName,
    AddressEventDto ShippingAddress,
    AddressEventDto BillingAddress,
    PaymentEventDto Payment,
    OrderStatus Status,
    List<OrderItemEventDto> OrderItems);

public record AddressEventDto(string FirstName, string LastName, string EmailAddress, string AddressLine, string Country, string State, string ZipCode);
public record PaymentEventDto(string CardName, string CardNumber, string Expiration, string Cvv, int PaymentMethod);
public record OrderItemEventDto(Guid OrderId, Guid ProductId, int Quantity, decimal Price);

public enum OrderStatus
{
    Draft = 1,
    Pending = 2,
    Completed = 3,
    Cancelled = 4
}
