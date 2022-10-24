using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JccApi.Models
{
    public class CreateFamilyRequest : BaseFamilyRequest
    {
        public List<CreateMemberRequest> Members { get; set; }

        public class CreateMemberRequest
        {
            public string Name { get; set; }
            public JccApi.Enums.LegalPersonType Type { get; set; }
        }
    }

    public class UpdateFamilyRequest : BaseFamilyRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
