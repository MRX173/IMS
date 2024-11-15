using Domain.Common;
using Domain.ValueObjects;
namespace Domain.Events;
public record LowStockEvent : DomainEvent
{
    public Guid ProductId { get; }
    public StockQuantity CurrentStock { get; }
    public StockQuantity ReorderLevel { get; }

    public LowStockEvent(
        Guid productId,
        StockQuantity currentStock,
        StockQuantity reorderLevel)
    {
        ProductId = productId;
        CurrentStock = currentStock;
        ReorderLevel = reorderLevel;
    }
}
