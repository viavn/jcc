using System.Collections.Generic;

namespace JccApi.Entities
{
    public class UserType : TypeEntityBase<int>
    {
        protected UserType() { }

        public UserType(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public ICollection<User> Users { get; private set; }
    }
}
