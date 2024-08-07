using AutoMapper;
using LoveStory.Core.DTOs;
using LoveStory.Infrastructure.Data;

namespace LoveStory.Core.Extensions;

public class Mapper : Profile
{
    public Mapper()
    {
        // DTO to Data
        CreateMap<UserDto, UserData>().ReverseMap();
        CreateMap<GuestSpecialNeedDto, GuestSpecialNeedData>()
            .ForMember(d=>d.Creator,opt=>opt.Ignore())
            .ForMember(d=>d.Guest,opt=>opt.Ignore())
            .ForMember(d=>d.CreatorId,opt=>opt.MapFrom(s=>s.Creator.UserId))
            .ForMember(d=>d.GuestId,opt=>opt.MapFrom(s=>s.Guest.GuestId))
            .ReverseMap();
        CreateMap<GuestAttendanceDto,GuestAttendanceData>().ReverseMap();
        CreateMap<GuestGroupDto,GuestGroupData>().ReverseMap();
        CreateMap<GuestDto,GuestData>()
            .ForMember(d=>d.Creator,opt=>opt.Ignore())
            .ForMember(d=>d.CreatorId ,opt=>opt.MapFrom(s=>s.Creator.UserId))
            .ReverseMap();
        CreateMap<BanquetTableDto,BanquetTableData>().ReverseMap();
    }
}