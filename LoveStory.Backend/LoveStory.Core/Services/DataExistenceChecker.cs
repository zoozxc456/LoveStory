using LoveStory.Core.Interfaces;
using LoveStory.Infrastructure.Data;
using LoveStory.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LoveStory.Core.Services;

public class DataExistenceChecker(IServiceProvider provider) : IGuestExistenceChecker, IGuestGroupExistenceChecker
{
    private readonly IRepository<GuestData> _guestRepository = provider.GetRequiredService<IRepository<GuestData>>();

    private readonly IGuestGroupRepository _guestGroupRepository =
        provider.GetRequiredService<IGuestGroupRepository>();

    public async Task<bool> IsGuestExistAsync(Guid guestId) =>
        await _guestRepository.GetOneAsync(guest => guest.GuestId == guestId) is not null;


    public async Task<bool> IsGuestGroupExistAsync(Guid groupId) =>
        await _guestGroupRepository.GetOneAsync(group => group.GuestGroupId == groupId) is not null;
}