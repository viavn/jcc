namespace JccApi.Entities.Base
{
    public abstract class TypeEntity<T>
    {
        public T Id { get; protected set; }
        public string Description { get; protected set; }
    }
}
