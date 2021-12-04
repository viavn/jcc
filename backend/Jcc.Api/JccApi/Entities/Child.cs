using JccApi.Models;
using System;
using System.Collections.Generic;

namespace JccApi.Entities
{
    public class Child
    {
        public Child() { }

        public Child(
            Guid id,
            string name,
            string age,
            string clothesSize,
            string shoeSize,
            string legalResponsible,
            string familyAcronym,
            string familyPhone,
            string familyAddress)
        {
            Id = id;
            Name = name;
            Age = age;
            ClothesSize = clothesSize;
            ShoesSize = shoeSize;
            LegalResponsible = legalResponsible;
            FamilyAcronym = familyAcronym;
            FamilyPhone = familyPhone;
            FamilyAddress = familyAddress;
        }

        public Child(
            string name,
            string age,
            string clothesSize,
            string shoeSize,
            string legalResponsible,
            string familyAcronym,
            string familyPhone,
            string familyAddress)
            : this(Guid.NewGuid(),
                  name,
                  age,
                  clothesSize,
                  shoeSize,
                  legalResponsible,
                  familyAcronym,
                  familyPhone,
                  familyAddress)
        {
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Age { get; private set; }
        public string ClothesSize { get; private set; }
        public string ShoesSize { get; private set; }
        public string LegalResponsible { get; private set; }
        public string FamilyAcronym { get; private set; }
        public string FamilyPhone { get; private set; }
        public string FamilyAddress { get; private set; }
        public ICollection<GodParent> GodParents { get; private set; } = new List<GodParent>();
        //public List<ChildGodParentItem> ChildGodParentItems { get; set; } = new List<ChildGodParentItem>();
    }
}
