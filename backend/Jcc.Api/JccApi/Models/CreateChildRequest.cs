using System;
using System.Text.Json.Serialization;
using JccApi.Enums;

namespace JccApi.Models
{
    public abstract class BaseChildRequest
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string ClotheSize { get; set; }
        public string ShoeSize { get; set; }
        public GenreType Genre { get; set; }
    }

    public class CreateChildRequest : BaseChildRequest
    {
        public Guid FamilyId { get; set; }
    }

    public class UpdateChildRequest : BaseChildRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
