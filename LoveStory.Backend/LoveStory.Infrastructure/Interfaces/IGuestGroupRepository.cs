using LoveStory.Infrastructure.Data;

namespace LoveStory.Infrastructure.Interfaces;

public interface IGuestGroupRepository
{
    Task<(bool, Guid)> CreateWithIdAsync(GuestGroupData entity);
}