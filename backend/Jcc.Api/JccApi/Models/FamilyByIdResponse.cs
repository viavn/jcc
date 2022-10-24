using System;
using System.Collections.Generic;

namespace JccApi.Models
{
    public class FamilyByIdResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }

        public IEnumerable<FamilyMemberResponse> Members { get; set; } = new List<FamilyMemberResponse>();
        public IEnumerable<ChildResponse> Children { get; set; } = new List<ChildResponse>();

        public class ChildResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Age { get; set; }
        }
    }
}
