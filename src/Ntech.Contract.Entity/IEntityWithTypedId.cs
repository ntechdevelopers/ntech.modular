namespace Ntech.Contract.Entity
{
    public interface IEntityWithTypedId<TId>
    {
        TId Id { get; }
    }
}
