namespace Domain.ValueObjects;

public record StockQuantity
{
    public int Value { get; }

    public StockQuantity(int value)
    {
        if (value < 0)
            throw new ArgumentException("Stock quantity cannot be negative", nameof(value));

        Value = value;
    }

    public StockQuantity Add(StockQuantity other) => new(Value + other.Value);
    public StockQuantity Subtract(StockQuantity other)
    {
        if (Value < other.Value)
            throw new InvalidOperationException("Cannot subtract to negative stock quantity");

        return new(Value - other.Value);
    }

    public static StockQuantity Zero => new(0);
    public static implicit operator int(StockQuantity quantity) => quantity.Value;
}
