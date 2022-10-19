using System.Collections.Generic;

namespace JccApi.Entities
{
    public class LegalPersonType : TypeEntityBase<int>
    {
        protected LegalPersonType() { }

        public LegalPersonType(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public ICollection<FamilyMember> Members { get; private set; }
    }
}
