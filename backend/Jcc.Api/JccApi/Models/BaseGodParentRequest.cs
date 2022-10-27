using System;
using System.Text.Json.Serialization;

namespace JccApi.Models
{
    public class BaseGodParentRequest
    {
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
    }

    public class UpdateGodParentRequest : BaseGodParentRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
