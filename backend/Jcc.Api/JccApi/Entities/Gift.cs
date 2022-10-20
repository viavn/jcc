using System;

namespace JccApi.Entities
{
    public class Gift
    {
        protected Gift() { }

        public Gift(Guid childId, Guid godParentId, Guid giftTypeId, DateTime createdDate, DateTime updatedDate, bool isDelivered, Guid userId)
        {
            ChildId = childId;
            GodParentId = godParentId;
            GiftTypeId = giftTypeId;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
            IsDelivered = isDelivered;
            UserId = userId;
        }

        public Guid ChildId { get; private set; }
        public Guid GodParentId { get; private set; }
        public Guid GiftTypeId { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime UpdatedDate { get; private set; }
        public bool IsDelivered { get; private set; }
        public Guid UserId { get; private set; }

        public Child Child { get; private set; }
        public GodParent GodParent { get; private set; }
        public GiftType Type { get; private set; }
        public User User { get; private set; }
    }
}
