using LoveStory.Core.DTOs.Recipient;
using LoveStory.Core.Interfaces;
using LoveStory.Infrastructure.Data;
using LoveStory.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LoveStory.Core.Services;

public class RecipientService(IServiceProvider provider) : IRecipientService
{
    private readonly IGuestExistenceChecker _guestExistenceChecker =
        provider.GetRequiredService<IGuestExistenceChecker>();

    private readonly IRepository<GuestAttendanceData> _guestAttendanceRepository =
        provider.GetRequiredService<IRepository<GuestAttendanceData>>();

    private readonly IRepository<GuestData> _guestRepository =
        provider.GetRequiredService<IRepository<GuestData>>();

    public List<GetRecipientGuestDto> GetRecipientGuests()
    {
        var guests = _guestRepository.GetAll().ToListAsync().Result;
        
        return guests.GroupBy(guest => new { guest.GuestGroupId }, (groupId, tGuests) =>
        {
            if (groupId.GuestGroupId is null)
            {
                return tGuests.Select(x => new GetRecipientGuestDto
                {
                    TargetId = x.GuestId,
                    GuestName = x.GuestName,
                    AttendanceAmount = 1,
                    Relationship = x.GuestRelationship
                }).ToList();
            }

            return
            [
                new GetRecipientGuestDto()
                {
                    TargetId = groupId.GuestGroupId.Value,
                    GuestName = tGuests.First().GuestGroup.GuestGroupName,
                    AttendanceAmount = tGuests.Count(),
                    Relationship = tGuests.First().GuestRelationship
                }
            ];
        }).SelectMany(x=>x).ToList();
    }

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