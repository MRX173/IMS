using Domain.Common;
namespace Domain.Events;
public record ProductDiscontinuedEvent : DomainEvent
{
    public Guid ProductId { get; }
    public string Reason { get; }

    public ProductDiscontinuedEvent(Guid productId, string reason)
    {
        ProductId = productId;
        Reason = reason;
    }
}
