using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Events;
public record ProductPriceUpdatedEvent : DomainEvent
{
    public Guid ProductId { get; }
    public Money NewPrice { get; }

    public ProductPriceUpdatedEvent(Guid productId, Money newPrice)
    {
        ProductId = productId;
        NewPrice = newPrice;
    }
}
