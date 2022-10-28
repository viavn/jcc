using System;
using System.Text.Json.Serialization;

namespace JccApi.Models
{
    public class CreateGiftRequest
    {
        [JsonIgnore]
        public Guid ChildId { get; set; }
        public Enums.GiftType Type { get; set; }
        public Guid UserId { get; set; }
        public BaseGodParentRequest GodParent { get; set; }
    }
    
    public class UpdateGiftRequest
    {
        [JsonIgnore]
        public Guid ChildId { get; set; }

        [JsonIgnore]
        public Enums.GiftType GiftType { get; set; }
        
        public Guid GodParentId { get; set; }
    }
}
