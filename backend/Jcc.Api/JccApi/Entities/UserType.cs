namespace JccApi.Entities
{
    public class UserType
    {
        public UserType() { }

        public UserType(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
    }
}
