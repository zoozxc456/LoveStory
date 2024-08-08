using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using LoveStory.Core.Interfaces;

namespace LoveStory.Core.Securities;

public class Argon2HashProvider : IHashProvider
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 4;
    private const int MemorySize = 65536;

    public byte[] CreateSalt()
    {
        return RandomNumberGenerator.GetBytes(SaltSize);
    }

    public string HashPassword(string password, byte[] saltBytes)
    {
        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));
        argon2.Salt = saltBytes;
        argon2.DegreeOfParallelism = 4;
        argon2.MemorySize = MemorySize;
        argon2.Iterations = Iterations;

        var hashBytes = argon2.GetBytes(HashSize);

        var saltedHashBytes = new byte[saltBytes.Length + hashBytes.Length];
        Buffer.BlockCopy(saltBytes, 0, saltedHashBytes, 0, saltBytes.Length);
        Buffer.BlockCopy(hashBytes, 0, saltedHashBytes, saltBytes.Length, hashBytes.Length);

        return Convert.ToBase64String(saltedHashBytes);
    }

    public bool VerifyPassword(string password, string storedSaltedHash)
    {
        var saltedHashBytes = Convert.FromBase64String(storedSaltedHash);

        var saltBytes = new byte[SaltSize];
        Buffer.BlockCopy(saltedHashBytes, 0, saltBytes, 0, saltBytes.Length);

        var storedHashBytes = new byte[HashSize];
        Buffer.BlockCopy(saltedHashBytes, saltBytes.Length, storedHashBytes, 0, storedHashBytes.Length);

        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));
        argon2.Salt = saltBytes;
        argon2.DegreeOfParallelism = 4;
        argon2.MemorySize = MemorySize;
        argon2.Iterations = Iterations;

        var computedHashBytes = argon2.GetBytes(HashSize);

        return CompareHashes(computedHashBytes, storedHashBytes);
    }

    public bool CompareHashes(byte[] hash1, byte[] hash2)
    {
        if (hash1.Length != hash2.Length)
        {
            return false;
        }

        return !hash1.Where((t, i) => t != hash2[i]).Any();
    }
}