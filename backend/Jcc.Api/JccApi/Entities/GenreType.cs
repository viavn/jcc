using System.Collections.Generic;

namespace JccApi.Entities
{
    public class GenreType : TypeEntityBase<int>
    {
        protected GenreType() { }

        public GenreType(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public ICollection<Child> Children { get; private set; }
    }
}
