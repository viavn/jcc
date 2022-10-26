using System;
using JccApi.Enums;

namespace JccApi.Models
{
    public class CreateChildRequest
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string ClotheSize { get; set; }
        public string ShoeSize { get; set; }
        public GenreType Genre { get; set; }
        public Guid FamilyId { get; set; }
    }
}
