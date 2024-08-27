using LoveStory.Core.DTOs;

namespace LoveStory.Core.Interfaces;

public interface IUserService
{
    public Task<IEnumerable<UserManagementDto>> GetAllUsersAsync();
    public Task<bool> CreateUserAsync(CreateUserDto dto);
    public Task<bool> ModifyUserBasicInfoAsync(ModifyUserBasicInfoDto dto);
    public Task<bool> ResetPasswordAsync(Guid userId);
    public Task<bool> DeleteUserByIdAsync(Guid userId);
}