using System;
using System.Text.Json.Serialization;

namespace JccApi.Models
{
    public class MemberRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public JccApi.Enums.LegalPersonType Type { get; set; }

        [JsonIgnore]
        public Guid FamilyId { get; set; }

        [JsonIgnore]
        public bool IsCreating { get; set; }
    }
}
