using System;
using System.Collections.Generic;

namespace JccApi.Models
{
    public record GodParentResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string ContactNumber { get; init; }
        public string Address { get; init; }
    }

    public record FamilyChildResponse
    {
        public Guid Id { get; init; }
        public string Code { get; init; }
        public string ContactNumber { get; init; }
        public string Address { get; init; }
        public string Member { get; init; }
    }

    public record GiftResponse
    {
        public bool IsDelivered { get; init; }
        public TypeResponse GiftType { get; init; }
        public GodParentResponse GodParent { get; init; }
    }

    public record GetChildrenByIdResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Age { get; init; }
        public string ClotheSize { get; init; }
        public string ShoeSize { get; init; }
        public FamilyChildResponse Family { get; init; }
        public TypeResponse Genre { get; init; }
        public IEnumerable<GiftResponse> Gifts { get; init; }
    }
}
