using JccApi.Entities.Base;

namespace JccApi.Entities
{
    public class GiftType : TypeEntity<int>
    {
        protected GiftType() { }

        public GiftType(int id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
