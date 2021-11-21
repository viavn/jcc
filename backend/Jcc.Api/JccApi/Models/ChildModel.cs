using System;
using System.Collections.Generic;

namespace JccApi.Models
{
    public class ChildModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string ClothesSize { get; set; }
        public string ShoeSize { get; set; }
        public string LegalResponsible { get; set; }
        public string FamilyAcronym { get; set; }
        public string FamilyPhone { get; set; }
        public string FamilyAddress { get; set; }
        public IEnumerable<GodParentModel> GodParents { get; set; } = new List<GodParentModel>();
    }
}
