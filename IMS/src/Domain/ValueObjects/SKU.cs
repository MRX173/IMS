namespace Domain.ValueObjects;
public record SKU
{
    public string Value { get; }

    public SKU(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("SKU cannot be empty", nameof(value));

        if (!IsValidSKUFormat(value))
            throw new ArgumentException("Invalid SKU format", nameof(value));

        Value = value.ToUpperInvariant();
    }

    private static bool IsValidSKUFormat(string sku)
    {
        return !string.IsNullOrWhiteSpace(sku) &&
               sku.Length <= 50 &&
               System.Text.RegularExpressions.Regex.IsMatch(sku, @"^[A-Za-z0-9\-]+$");
    }
}
