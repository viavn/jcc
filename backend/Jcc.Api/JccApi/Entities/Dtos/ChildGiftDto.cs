using System;

namespace JccApi.Entities.Dtos
{
    public record ChildGiftDto(
        Guid Id,
        string Name,
        string FamilyCode,
        int DeliveredGifts,
        int RemainingGifts,
        string Age,
        string ShoeSize,
        string ClotheSize
    );
}