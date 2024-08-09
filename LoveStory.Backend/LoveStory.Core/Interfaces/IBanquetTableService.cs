using LoveStory.Core.DTOs;

namespace LoveStory.Core.Interfaces;

public interface IBanquetTableService
{
    public IEnumerable<BanquetTableDto> GetAllBanquetTables();
}