using System;
using System.Collections.Generic;

namespace JccApi.Entities
{
    public class Family
    {
        protected Family() { }

        public Guid Id { get; private set; }
        public string Code { get; private set; }
        public string ContactNumber { get; private set; }
        public string Address { get; private set; }
        public string Comment { get; private set; }
        public ICollection<FamilyMember> Members { get; private set; }
        public ICollection<Child> Children { get; private set; }
    }
}
