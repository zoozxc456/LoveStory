using LoveStory.Core.DTOs;

namespace LoveStory.Core.Interfaces;

public interface IUserService
{
    public Task<IEnumerable<UserManagementDto>> GetAllUsersAsync();
    public Task<bool> CreateUserAsync(CreateUserDto dto);
}