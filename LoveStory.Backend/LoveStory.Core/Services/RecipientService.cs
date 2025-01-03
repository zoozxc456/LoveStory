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

    private readonly IGuestGroupExistenceChecker _guestGroupExistenceChecker =
        provider.GetRequiredService<IGuestGroupExistenceChecker>();

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
                    Relationship = x.GuestRelationship,
                    ArrivedAt = x.GuestAttendance?.ArrivalAt
                }).ToList();
            }

            return
            [
                new GetRecipientGuestDto
                {
                    TargetId = groupId.GuestGroupId.Value,
                    GuestName = tGuests.First().GuestGroup.GuestGroupName,
                    AttendanceAmount = tGuests.Count(),
                    Relationship = tGuests.First().GuestRelationship,
                    ArrivedAt = tGuests.First().GuestAttendance?.ArrivalAt
                }
            ];
        }).SelectMany(x => x).ToList();
    }

    public GetRecipientGuestOverviewDto GetRecipientGuestOverview(Guid targetId, string guestType)
    {
        if (guestType.Equals("single"))
        {
            if (_guestExistenceChecker.IsGuestExistAsync(targetId).Result)
            {
                var guest = _guestRepository.GetAll().ToList().FirstOrDefault(x => x.GuestId == targetId);
                if (guest is null) throw new Exception();

                return new GetRecipientGuestOverviewDto
                {
                    SpecialNeeds = guest.SpecialNeeds.Select(x => x.SpecialNeedContent).ToList(),
                    Remark = guest.Remark,
                    SeatLocation = guest.SeatLocation?.TableAlias,
                    TargetId = targetId,
                    GuestName = guest.GuestName,
                    AttendanceAmount = 1,
                    Relationship = guest.GuestRelationship,
                    ArrivedAt = guest.GuestAttendance?.ArrivalAt
                };
            }
            else
            {
                throw new Exception($"This Guest Is Not Exist, GuestId: {targetId}");
            }
        }


        if (_guestGroupExistenceChecker.IsGuestGroupExistAsync(targetId).Result)
        {
            var guestDatas = _guestRepository.GetAll().ToList().Where(x => x.GuestGroupId == targetId).ToList();

            return new GetRecipientGuestOverviewDto
            {
                SpecialNeeds = guestDatas.SelectMany(x => x.SpecialNeeds.Select(y => y.SpecialNeedContent).ToList())
                    .ToList(),
                Remark = string.Join(" / ", guestDatas.Select(x => $"{x.GuestName}:{x.Remark}").ToList()),
                SeatLocation = string.Join(" / ",
                    guestDatas.Select(x => $"{x.GuestName}:{x.SeatLocation?.TableAlias ?? string.Empty}").ToList()),
                TargetId = targetId,
                GuestName = guestDatas[0].GuestGroup.GuestGroupName,
                AttendanceAmount = guestDatas.Count,
                Relationship = guestDatas[0].GuestRelationship,
                ArrivedAt = guestDatas[0].GuestAttendance.ArrivalAt
            };
        }
        throw new Exception($"This Guest Group Is Not Exist, GroupId: {targetId}");
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

    public bool RegisterGuestAttendance(RegisterGuestGroupAttendanceDto dto)
    {
        if (_guestGroupExistenceChecker.IsGuestGroupExistAsync(dto.GuestGroupId).Result)
        {
            _guestAttendanceRepository.InsertMultipleAsync(_guestRepository.GetAll()
                .Where(x => x.GuestGroupId == dto.GuestGroupId)
                .ToList()
                .Select(x =>
                    new GuestAttendanceData
                    {
                        GuestId = x.GuestId,
                        ArrivalAt = DateTime.Now,
                        CreatorId = dto.CreatorId
                    })
                .ToList());
        }

        throw new Exception($"This Guest Group Is Not Exist, GroupId: {dto.GuestGroupId}");
    }
}