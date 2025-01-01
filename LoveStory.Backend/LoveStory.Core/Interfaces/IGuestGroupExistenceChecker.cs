namespace LoveStory.Core.Interfaces;

public interface IGuestGroupExistenceChecker
{
    public Task<bool> IsGuestGroupExistAsync(Guid groupId);
}