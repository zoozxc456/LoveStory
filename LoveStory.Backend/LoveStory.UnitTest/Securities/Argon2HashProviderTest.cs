using System.Text;
using LoveStory.Core.Securities;

namespace LoveStory.UnitTest.Securities;

[TestFixture]
public class Argon2HashProviderTest
{
    private readonly Argon2HashProvider _hashProvider;
    private string _rawPassword;
    private string _hashedPassword;
    private byte[] _salt;

    public Argon2HashProviderTest()
    {
        _hashProvider = new Argon2HashProvider();
    }

    [Test]
    [TestCase("admin", "MXjfF72e8Y1fZJ4P", "TVhqZkY3MmU4WTFmWko0UFELTaXApm/xXSLEf4H/V6UG+BEXI8xPF7o5o4vf2v3j")]
    public void HashPassword_GivenSpecificPasswordAndSaltString_ShouldReturnHashedPassword(string password,
        string saltString, string expected)
    {
        SetRawPassword(password);
        SetSalt(saltString);

        var hashPassword = _hashProvider.HashPassword(_rawPassword, _salt);

        Assert.That(hashPassword, Is.EqualTo(expected));
    }

    [TestCase("admin", "TVhqZkY3MmU4WTFmWko0UFELTaXApm/xXSLEf4H/V6UG+BEXI8xPF7o5o4vf2v3j")]
    public void VerifyPassword_GivenCorrectSpecificPasswordAndHashedPassword_ShouldReturnTrue(string password,
        string hashedPassword)
    {
        SetRawPassword(password);
        SetHashedPassword(hashedPassword);

        var result = _hashProvider.VerifyPassword(_rawPassword, _hashedPassword);

        Assert.That(result, Is.True);
    }

    [TestCase("hello", "TVhqZkY3MmU4WTFmWko0UFELTaXApm/xXSLEf4H/V6UG+BEXI8xPF7o5o4vf2v3j")]
    public void VerifyPassword_GivenErrorSpecificPasswordAndHashedPassword_ShouldReturnFalse(string password,
        string hashedPassword)
    {
        SetRawPassword(password);
        SetHashedPassword(hashedPassword);

        var result = _hashProvider.VerifyPassword(_rawPassword, _hashedPassword);

        Assert.That(result, Is.False);
    }

    [Test]
    public void CompareHashes_GivenTwoSameBytesArray_ShouldReturnTrue()
    {
        var hashed1 = new byte[] { 0x01, 0x02, 0x03, 0x04 };
        var hashed2 = new byte[] { 0x01, 0x02, 0x03, 0x04 };

        var result = _hashProvider.CompareHashes(hashed1, hashed2);

        Assert.That(result, Is.True);
    }

    [Test]
    public void CompareHashes_GivenTwoDifferentBytesArray_ShouldReturnFalse()
    {
        var hashed1 = new byte[] { 0x01, 0x02, 0x03, 0x04 };
        var hashed2 = new byte[] { 0x01, 0x02, 0x05, 0x10 };

        var result = _hashProvider.CompareHashes(hashed1, hashed2);

        Assert.That(result, Is.False);
    }

    [Test]
    public void CreateSalt_GivenEmptyParameter_ShouldReturnBytesOfSize16()
    {
        var size = _hashProvider.CreateSalt().Length;

        Assert.That(size, Is.EqualTo(16));
    }

    private void SetRawPassword(string rawPassword) => _rawPassword = rawPassword;
    private void SetHashedPassword(string hashedPassword) => _hashedPassword = hashedPassword;
    private void SetSalt(string saltString) => _salt = Encoding.UTF8.GetBytes(saltString);
}