namespace LoveStory.Core.Interfaces;

public interface IHashProvider
{
    public byte[] CreateSalt();
    public string HashPassword(string password, byte[] saltBytes);
    public bool VerifyPassword(string password, string storedSaltedHash);
}