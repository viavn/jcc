namespace JccApi.Entities
{
    public abstract class TypeEntityBase<T>
    {
        public T Id { get; protected set; }
        public string Description { get; protected set; }
    }
}
