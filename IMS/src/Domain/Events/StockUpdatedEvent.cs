using Domain.Common;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Events;
public record StockUpdatedEvent : DomainEvent
{
    public Guid ProductId { get; }
    public StockQuantity Quantity { get; }
    public InventoryMovementType MovementType { get; }
    public string Reason { get; }
    public StockQuantity NewStockLevel { get; }

    public StockUpdatedEvent(
        Guid productId,
        StockQuantity quantity,
        InventoryMovementType movementType,
        string reason,
        StockQuantity newStockLevel)
    {
        ProductId = productId;
        Quantity = quantity;
        MovementType = movementType;
        Reason = reason;
        NewStockLevel = newStockLevel;
    }
}
