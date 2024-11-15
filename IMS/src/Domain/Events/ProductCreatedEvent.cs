using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Events;
public record ProductCreatedEvent : DomainEvent
{
    public Guid ProductId { get; }
    public SKU Sku { get; }
    public string Name { get; }
    public string Description { get; }
    public Money UnitPrice { get; }
    public StockQuantity ReorderLevel { get; }
    public CategoryId CategoryId { get; }
    public SupplierId SupplierId { get; }

    public ProductCreatedEvent(
        Guid productId,
        SKU sku,
        string name,
        string description,
        Money unitPrice,
        StockQuantity reorderLevel,
        CategoryId categoryId,
        SupplierId supplierId)
    {
        ProductId = productId;
        Sku = sku;
        Name = name;
        Description = description;
        UnitPrice = unitPrice;
        ReorderLevel = reorderLevel;
        CategoryId = categoryId;
        SupplierId = supplierId;
    }
}
