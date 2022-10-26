using System;
using System.Collections.Generic;

namespace JccApi.Entities
{
    public class Child
    {
        protected Child() { }

        public Child(Guid id)
        {
            Id = id;
        }

         public Child(string name, string age, string clotheSize, string shoeSize, int genreTypeId, Guid familyId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Age = age;
            ClotheSize = clotheSize;
            ShoeSize = shoeSize;
            GenreTypeId = genreTypeId;
            FamilyId = familyId;
        }

        public Child(Guid id, string name, string age, string clotheSize, string shoeSize, int genreTypeId, Guid familyId)
        {
            Id = id;
            Name = name;
            Age = age;
            ClotheSize = clotheSize;
            ShoeSize = shoeSize;
            GenreTypeId = genreTypeId;
            FamilyId = familyId;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Age { get; private set; }
        public string ClotheSize { get; private set; }
        public string ShoeSize { get; private set; }
        public int GenreTypeId { get; private set; }
        public Guid FamilyId { get; private set; }

        public Genre GenreType { get; private set; }
        public Family Family { get; private set; }
        public ICollection<Gift> Gifts { get; private set; }
    }
}
