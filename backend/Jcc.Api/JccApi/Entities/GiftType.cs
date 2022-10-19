namespace JccApi.Entities
{
    public class GiftType : TypeEntityBase<int>
    {
        protected GiftType() { }

        public GiftType(int id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
