using Domain.Common;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;
public class InventoryMovement : Entity
{
    public StockQuantity Quantity { get; private set; }
    public InventoryMovementType MovementType { get; private set; }
    public string Reason { get; private set; }
    public DateTime Timestamp { get; private set; }

    private InventoryMovement() { } // For EF Core

    public InventoryMovement(
        StockQuantity quantity,
        InventoryMovementType movementType,
        string reason)
    {
        Id = Guid.NewGuid();
        Quantity = quantity;
        MovementType = movementType;
        Reason = reason;
        Timestamp = DateTime.UtcNow;
    }
}
