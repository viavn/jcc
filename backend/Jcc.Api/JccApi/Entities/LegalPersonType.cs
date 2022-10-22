using JccApi.Entities.Base;

namespace JccApi.Entities
{
    public class LegalPersonType : TypeEntity<int>
    {
        protected LegalPersonType() { }

        public LegalPersonType(int id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}