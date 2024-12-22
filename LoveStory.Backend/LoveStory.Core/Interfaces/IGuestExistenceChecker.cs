namespace LoveStory.Core.Interfaces;

public interface IGuestExistenceChecker
{
    public Task<bool> IsGuestExistAsync(Guid guestId);
}