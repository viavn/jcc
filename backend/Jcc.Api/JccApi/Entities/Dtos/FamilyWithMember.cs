using System;

namespace JccApi.Entities.Dtos
{
    public class FamilyWithMember
    {
        public FamilyWithMember(Guid id, string code, string address, string memberName)
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