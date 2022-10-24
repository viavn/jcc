using System;

namespace JccApi.Models
{
    public class FamilyResponse
    {
        public FamilyResponse(Guid id, string code, string address, string memberName)
        {
            Id = id;
            Code = code;
            Address = address;
            MemberName = memberName;
        }

        public Guid Id { get; }
        public string Code { get; }
        public string Address { get; }
        public string MemberName { get; }
    }
}
