using System;

namespace JccApi.Entities
{
    public class FamilyMember
    {
        protected FamilyMember() { }

        public FamilyMember(Guid id)
        {
            Id = id;
        }

        public FamilyMember(string name, int legalPersonTypeId, Guid familyId)
        {
            Id = Guid.NewGuid();
            Name = name;
            LegalPersonTypeId = legalPersonTypeId;
            FamilyId = familyId;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int LegalPersonTypeId { get; private set; }
        public Guid FamilyId { get; private set; }

        public LegalPersonType LegalPersonType { get; private set; }
        public Family Family { get; private set; }
    }
}
