using System;

namespace JccApi.Entities
{
    public class FamilyMember
    {
        protected FamilyMember() { }

        public FamilyMember(Guid id, string name, int legalPersonTypeId, Guid familyId)
        {
            Id = id;
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
