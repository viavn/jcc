using System;

namespace JccApi.Entities
{
    public class Gift
    {
        protected Gift() { }

        public Gift(Guid childId, Guid godParentId)
        {
            ChildId = childId;
            GodParentId = GodParentId;
        }

        public Gift(Guid childId, Guid godParentId, int typeId, DateTime createdDate, DateTime updatedDate, Guid userId)
        {
            ChildId = childId;
            GodParentId = godParentId;
            TypeId = typeId;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
            UserId = userId;
            IsDelivered = false;
        }

        public Guid ChildId { get; private set; }
        public Guid GodParentId { get; private set; }
        public int TypeId { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime UpdatedDate { get; private set; }
        public bool IsDelivered { get; private set; }
        public Guid UserId { get; private set; }

        public Child Child { get; private set; }
        public GodParent GodParent { get; private set; }
        public GiftType Type { get; private set; }
        public User User { get; private set; }

        public void Update()
        {
            UpdatedDate = DateTime.Now;
        }
    }
}
