using System;

namespace JccApi.Models
{
    public class GodParentModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool IsClothesSelected { get; set; }
        public bool IsShoeSelected { get; set; }
        public bool IsGiftSelected { get; set; }
    }
}
