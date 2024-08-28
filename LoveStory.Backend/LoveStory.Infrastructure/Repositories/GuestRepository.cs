using System.Linq.Expressions;
using LoveStory.Infrastructure.Contexts;
using LoveStory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LoveStory.Infrastructure.Repositories;

public class GuestRepository(LoveStoryContext context) : GenericRepository<GuestData>(context)
{
    private readonly LoveStoryContext _context = context;

    public override IQueryable<GuestData> GetAll()
    {
        return _context.Guests
            .Include(x => x.GuestAttendance)
            .Include(x => x.GuestGroup)
            .Include(x => x.SeatLocation)
            .Include(x => x.SpecialNeeds)
            .Include(x => x.Creator);
    }
}