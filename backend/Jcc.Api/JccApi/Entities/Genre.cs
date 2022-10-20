using System.Collections.Generic;
using JccApi.Entities.Base;

namespace JccApi.Entities
{
    public class Genre : TypeEntity<int>
    {
        protected Genre() { }

        public Genre(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public ICollection<Child> Children { get; private set; }
    }
}
