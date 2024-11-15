using Domain.Entities;
using Domain.Enums;
using Domain.Events;
using Domain.ValueObjects;

namespace Domain.Common;
public class Product : AggregateRoot
{
    public SKU Sku { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Money UnitPrice { get; private set; }
    public StockQuantity CurrentStock { get; private set; }
    public StockQuantity ReorderLevel { get; private set; }
    public ProductStatus Status { get; private set; }
    public CategoryId CategoryId { get; private set; }
    public SupplierId SupplierId { get; private set; }

    private readonly List<InventoryMovement> _movements = new();
    public IReadOnlyCollection<InventoryMovement> Movements => _movements.AsReadOnly();

    private Product() { } // For EF Core

    public Product(
        SKU sku,
        string name,
        string description,
        Money unitPrice,
        StockQuantity reorderLevel,
        CategoryId categoryId,
        SupplierId supplierId)
    {
        Apply(new ProductCreatedEvent(
            Guid.NewGuid(),
            sku,
            name,
            description,
            unitPrice,
            reorderLevel,
            categoryId,
            supplierId));
    }

    public void UpdateStock(StockQuantity quantity, InventoryMovementType movementType, string reason)
    {
        var newStock = movementType switch
        {
            InventoryMovementType.Receipt or
            InventoryMovementType.Return => CurrentStock.Add(quantity),

            InventoryMovementType.Shipment or
            InventoryMovementType.Damage => CurrentStock.Subtract(quantity),

            InventoryMovementType.Adjustment => new StockQuantity(quantity.Value),

            _ => throw new ArgumentException("Invalid movement type", nameof(movementType))
        };

        Apply(new StockUpdatedEvent(
            Id,
            quantity,
            movementType,
            reason,
            newStock));

        if (newStock.Value <= ReorderLevel.Value)
        {
            Apply(new LowStockEvent(Id, newStock, ReorderLevel));
        }
    }

    public void UpdatePrice(Money newPrice)
    {
        if (newPrice.Currency != UnitPrice.Currency)
            throw new InvalidOperationException("Cannot change price currency");

        Apply(new ProductPriceUpdatedEvent(Id, newPrice));
    }

    public void Discontinue(string reason)
    {
        if (Status == ProductStatus.Discontinued)
            throw new InvalidOperationException("Product is already discontinued");

        Apply(new ProductDiscontinuedEvent(Id, reason));
    }

    protected override void When(DomainEvent @event)
    {
        switch (@event)
        {
            case ProductCreatedEvent e:
                Id = e.ProductId;
                Sku = e.Sku;
                Name = e.Name;
                Description = e.Description;
                UnitPrice = e.UnitPrice;
                ReorderLevel = e.ReorderLevel;
                CategoryId = e.CategoryId;
                SupplierId = e.SupplierId;
                CurrentStock = StockQuantity.Zero;
                Status = ProductStatus.Active;
                break;

            case StockUpdatedEvent e:
                CurrentStock = e.NewStockLevel;
                _movements.Add(new InventoryMovement(
                    e.Quantity,
                    e.MovementType,
                    e.Reason));
                break;

            case ProductPriceUpdatedEvent e:
                UnitPrice = e.NewPrice;
                break;

            case ProductDiscontinuedEvent e:
                Status = ProductStatus.Discontinued;
                break;
        }
    }
}
