using System;

namespace JccApi.Models
{
    public record GetChildrenByIdResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Age { get; init; }
        public string ClotheSize { get; init; }
        public string ShoeSize { get; init; }
        public string FamilyCode { get; init; }
        public TypeResponse Genre { get; init; }
    }
}
