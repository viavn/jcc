using System;

namespace JccApi.Models
{
    public class FamilyMemberResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TypeResponse LegalPerson { get; set; }
    }
}
