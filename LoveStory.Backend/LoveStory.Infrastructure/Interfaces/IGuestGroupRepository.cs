using LoveStory.Infrastructure.Data;

namespace LoveStory.Infrastructure.Interfaces;

public interface IGuestGroupRepository:IRepository<GuestGroupData>
{
    Task<(bool, Guid)> CreateWithIdAsync(GuestGroupData entity);
}