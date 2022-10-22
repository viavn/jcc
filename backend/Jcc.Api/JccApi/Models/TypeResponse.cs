namespace JccApi.Models
{
    public class TypeResponse
    {
        public TypeResponse(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public int Id { get; }
        public string Description { get; }
    }
}
