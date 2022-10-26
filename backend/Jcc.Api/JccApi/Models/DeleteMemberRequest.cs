using System;

namespace JccApi.Models
{
    public class DeleteMemberRequest
    {
        public Guid MemberId { get; set; }
        public Guid FamilyId { get; set; }
    }
}
