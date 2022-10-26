using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JccApi.Models
{
    public class CreateFamilyRequest : BaseFamilyRequest
    {
        public List<MemberRequest> Members { get; set; }
    }

    public class UpdateFamilyRequest : BaseFamilyRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
