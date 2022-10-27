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
}
