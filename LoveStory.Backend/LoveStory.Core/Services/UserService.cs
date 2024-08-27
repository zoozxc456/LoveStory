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

    public Task<IEnumerable<UserManagementDto>> GetAllUsersAsync()
    {
        return Task.FromResult<IEnumerable<UserManagementDto>>(_userRepository.GetAll()
            .Select(x => _mapper.Map<UserManagementDto>(x)));
    }
}