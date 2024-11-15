namespace Domain.Common;

public abstract class AggregateRoot
{
    private readonly List<DomainEvent> _uncommittedEvents = new();
    private int _version = -1;

    public Guid Id { get; protected set; }
    public int Version => _version;

    protected void Apply(DomainEvent @event)
    {
        _version++;
        @event.Version = _version;
        _uncommittedEvents.Add(@event);
        When(@event);
    }

    protected abstract void When(DomainEvent @event);

    public IEnumerable<DomainEvent> GetUncommittedEvents()
    {
        return _uncommittedEvents.AsReadOnly();
    }

    public void ClearUncommittedEvents()
    {
        _uncommittedEvents.Clear();
    }
}
