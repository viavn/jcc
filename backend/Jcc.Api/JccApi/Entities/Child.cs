using JccApi.Models;
using System;
using System.Collections.Generic;

namespace JccApi.Entities
{
    public class Child_Old
    {
        public Child_Old() { }

        public Child_Old(
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

        public Child_Old(
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
        public ICollection<GodParent_Old> GodParents { get; private set; } = new List<GodParent_Old>();
        //public List<ChildGodParentItem> ChildGodParentItems { get; set; } = new List<ChildGodParentItem>();
    }

    public class Child
    {
        protected Child() { }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Age { get; private set; }
        public string ClotheSize { get; private set; }
        public string ShoeSize { get; private set; }

        public int GenreTypeId { get; private set; }
        public GenreType GenreType { get; private set; }

        public Guid FamilyId { get; private set; }
        public Family Family { get; private set; }
        public ICollection<Gift> Gifts { get; private set; }
    }
}
