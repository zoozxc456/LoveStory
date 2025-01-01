using LoveStory.Core.DTOs;

namespace LoveStory.Core.Interfaces;

public interface ILoginService
{
    public Task<(bool, string?)> Login(LoginRequestDto requestDto);
    public Task<(bool, string?)> Login(DevelopLoginRequestDto requestDto);
}