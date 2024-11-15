using Domain.Common;

namespace Domain.Entities;

public class Category : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    private Category() { } // For EF Core

    public Category(string name, string description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
    }
}
