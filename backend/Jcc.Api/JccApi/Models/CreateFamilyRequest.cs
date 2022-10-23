using System.Collections.Generic;

namespace JccApi.Models
{
    public class CreateFamilyRequest
    {
        public string Code { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }

        public List<CreateMemberRequest> Members { get; set; }

        public class CreateMemberRequest
        {
            public string Name { get; private set; }
            public int LegalPersonTypeId { get; private set; }
        }
    }
}
