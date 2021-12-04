using System;

namespace JccApi.Entities
{
    public class GodParent
    {
        public GodParent() { }

        public GodParent(
            Guid id,
            string name,
            string phone,
            bool isClothesSelected,
            bool isShoeSelected,
            bool isGiftSelected,
            DateTime createdDate,
            Guid userWhoCreated,
            Guid childId)
        {
            Id = id;
            Name = name;
            Phone = phone;
            IsClothesSelected = isClothesSelected;
            IsShoesSelected = isShoeSelected;
            IsGiftSelected = isGiftSelected;
            CreatedDate = createdDate;
            UserId = userWhoCreated;
            ChildId = childId;
        }

        public GodParent(
            string name,
            string phone,
            bool isClothesSelected,
            bool isShoeSelected,
            bool isGiftSelected,
            DateTime createdDate,
            Guid userWhoCreated,
            Guid childId)
            :
            this(Guid.NewGuid(),
                name,
                phone,
                isClothesSelected,
                isShoeSelected,
                isGiftSelected,
                createdDate,
                userWhoCreated,
                childId)
        {
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Phone { get; private set; }
        public DateTime CreatedDate { get; set; }
        public bool IsClothesSelected { get; private set; }
        public bool IsShoesSelected { get; private set; }
        public bool IsGiftSelected { get; private set; }
        public Guid ChildId { get; private set; }
        public Child Child { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        //public List<ChildGodParentItem> ChildGodParentItems { get; set; } = new List<ChildGodParentItem>();
    }
}
