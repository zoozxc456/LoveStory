using LoveStory.Infrastructure.Contexts;
using LoveStory.Infrastructure.Data;
using LoveStory.Infrastructure.Interfaces;

namespace LoveStory.Infrastructure.Repositories;

public class GuestGroupRepository(LoveStoryContext context) : GenericRepository<GuestGroupData>(context), IGuestGroupRepository
{
    public async Task<(bool, Guid)> CreateWithIdAsync(GuestGroupData entity)
    {
        var isSuccess = await InsertAsync(entity);
        return isSuccess ? (isSuccess, entity.GuestGroupId) : (isSuccess, Guid.Empty);
    }
}