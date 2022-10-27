using System;
using System.Collections.Generic;

namespace JccApi.Entities
{
    public class GodParent
    {
        protected GodParent() { }

        public GodParent(Guid id)
        {
            Id = id;
        }

        public GodParent(string name, string contactNumber, string address)
        {
            Id = Guid.NewGuid();
            Name = name;
            ContactNumber = contactNumber;
            Address = address;
        }

        public GodParent(Guid id, string name, string contactNumber, string address)
        {
            Id = id;
            Name = name;
            ContactNumber = contactNumber;
            Address = address;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string ContactNumber { get; private set; }
        public string Address { get; private set; }
        public ICollection<Gift> Gifts { get; private set; }

        public void Update(string name, string contactNumber, string address)
        {
            Name = name;
            ContactNumber = contactNumber;
            Address = address;
        }
    }
}
