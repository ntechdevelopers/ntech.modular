namespace Ntech.Contract.Entity
{
    public class BaseEntityWithTypedId<TId> : IEntityWithTypedId<TId>
    {
        public TId Id { get; protected set; }
    }
}
