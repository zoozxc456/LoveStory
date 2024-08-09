using AutoMapper;
using LoveStory.Core.DTOs;
using LoveStory.Core.Interfaces;
using LoveStory.Infrastructure.Data;
using LoveStory.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LoveStory.Core.Services;

public class BanquetTableService(IServiceProvider provider) : IBanquetTableService
{
    private readonly IRepository<BanquetTableData> _banquetTableDataRepository =
        provider.GetRequiredService<IRepository<BanquetTableData>>();

    private readonly IMapper _mapper = provider.GetRequiredService<IMapper>();

    public IEnumerable<BanquetTableDto> GetAllBanquetTables()
    {
        return _banquetTableDataRepository.GetAll().ToList()
            .Select(table => _mapper.Map<BanquetTableDto>(table));
    }
}