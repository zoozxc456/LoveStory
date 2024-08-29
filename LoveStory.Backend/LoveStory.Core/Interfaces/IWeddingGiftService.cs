using LoveStory.Core.DTOs;

namespace LoveStory.Core.Interfaces;

public interface IWeddingGiftManagementService
{
    IEnumerable<WeddingGiftManagementDto> Get();
}