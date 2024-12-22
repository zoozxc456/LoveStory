using LoveStory.Core.DTOs.Recipient;
using LoveStory.Core.Interfaces;
using LoveStory.Infrastructure.Data;
using LoveStory.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LoveStory.Core.Services;

public class RecipientService(IServiceProvider provider) : IRecipientService
{
    private readonly IGuestExistenceChecker _guestExistenceChecker =
        provider.GetRequiredService<IGuestExistenceChecker>();

    private readonly IRepository<GuestAttendanceData> _guestAttendanceRepository =
        provider.GetRequiredService<IRepository<GuestAttendanceData>>();


    public bool RegisterGuestAttendance(RegisterGuestAttendanceDto dto)
    {
        if (_guestExistenceChecker.IsGuestExistAsync(dto.GuestId).Result)
        {
            return _guestAttendanceRepository.InsertAsync(new GuestAttendanceData
            {
                GuestId = dto.GuestId,
                ArrivalAt = DateTime.Now,
                CreatorId = dto.CreatorId
            }).Result;
        }

        throw new Exception($"This Guest Is Not Exist, GuestId: {dto.GuestId}");
    }
}