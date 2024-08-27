using AutoMapper;
using LoveStory.Core.DTOs;
using LoveStory.Core.Interfaces;
using LoveStory.Infrastructure.Data;
using LoveStory.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LoveStory.Core.Services;

public class UserService(IServiceProvider provider) : IUserService
{
    private readonly IMapper _mapper = provider.GetRequiredService<IMapper>();
    private readonly IRepository<UserData> _userRepository = provider.GetRequiredService<IRepository<UserData>>();
    private readonly IHashProvider _hashProvider = provider.GetRequiredService<IHashProvider>();

    public Task<IEnumerable<UserManagementDto>> GetAllUsersAsync()
    {
        return Task.FromResult<IEnumerable<UserManagementDto>>(_userRepository.GetAll()
            .Select(x => _mapper.Map<UserManagementDto>(x)));
    }

    public async Task<bool> CreateUserAsync(CreateUserDto dto)
    {
        if (await IsExistSameUsernameUser(dto.Username)) throw new Exception("It exist the Same Username User.");

        var (saltedString, hashedPassword) = GenerateDefaultPassword(dto.Username);

        return await _userRepository.InsertAsync(new UserData
        {
            Username = dto.Username,
            Role = dto.Role,
            Password = hashedPassword,
            Salted = saltedString,
        });
    }

    public async Task<bool> ModifyUserBasicInfoAsync(ModifyUserBasicInfoDto dto)
    {
        var user = await _userRepository.GetOneAsync(user => user.UserId == dto.UserId);
        if (user == null) throw new Exception($"UserId: {dto.UserId} is not exist.");

        user.Username = dto.Username;
        user.Role = dto.Role;

        return await _userRepository.UpdateAsync(user);
    }

    private async Task<bool> IsExistSameUsernameUser(string username) =>
        await _userRepository.GetOneAsync(x => x.Username == username) != null;

    private (string saltedString, string) GenerateDefaultPassword(string username)
    {
        var salted = _hashProvider.CreateSalt();
        var defaultPassword = $"Default_Password_{username}";
        return (salted.ToString()!, _hashProvider.HashPassword(defaultPassword, salted));
    }
}