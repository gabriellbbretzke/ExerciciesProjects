﻿using JwtStore.Core.Contexts.SharedContext.ValueObjects;
using System.Security.Cryptography;

namespace JwtStore.Core.Contexts.AccountContext.ValueObjects;

public class Password : ValueObject
{
    private const string Valid = "abcdefghijklmnopqrstuvvyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"; 
    private const string Special = "!@#$%^&*(){}[];";

    protected Password()
    {
    }

    public Password(string? text = null)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            text = Generate();

        Hash = Hashing(text);
    }

    public bool Challenge(string plainTextPassword)
        => Verify(Hash, plainTextPassword);

    public string Hash { get; } = string.Empty;
    public string ResetCode { get; } = Guid.NewGuid().ToString("N")[..8].ToUpper();

    private static string Generate(
        short length = 16,
        bool includeSpecialChars = true,
        bool upperCase = false)
    {
        var chars = includeSpecialChars ? Valid + Special : Valid;
        var startRandom = upperCase ? 26 : 0;
        var index = 0;
        var res = new char[length];
        var rnd = new Random();

        while (index < length)
            res[index++] = chars[rnd.Next(startRandom, chars.Length)];

        return new string(res);
    }

    /// <summary>
    /// Hashes as password
    /// </summary>
    /// <param name="password"></param>
    /// <param name="saltSize"></param>
    /// <param name="keySize"></param>
    /// <param name="iterations"></param>
    /// <param name="splitChar"></param>
    /// <param name="privateKey"></param>
    /// <returns></returns>
    /// <exception cref="InvalidPasswordException"></exception>
    public static string Hashing(
        string password,
        short saltSize = 16,
        short keySize = 32,
        int iterations = 10000,
        char splitChar = '.',
        string privateKey = "")
    {
        if (string.IsNullOrEmpty(password))
            throw new Exception("Password should not be null or empty");

        password += Configuration.Secrets.PasswordSaltKey;

        using var algorithm = new Rfc2898DeriveBytes(
            password,
            saltSize,
            iterations,
            HashAlgorithmName.SHA256);
        var key = Convert.ToBase64String(algorithm.GetBytes(keySize));
        var salt = Convert.ToBase64String(algorithm.Salt);

        return $"{iterations}{splitChar}{salt}{splitChar}{key}";
    }

    public static bool Verify(
       string hash,
       string password,
       short keySize = 32,
       int iterations = 10000,
       char splitChar = '.')
    {
        password += Configuration.Secrets.PasswordSaltKey;

        var parts = hash.Split(splitChar, 3);
        if (parts.Length != 3)
            return false;

        var hashIterations = Convert.ToInt32(parts[0]);
        var salt = Convert.FromBase64String(parts[1]);
        var key = Convert.FromBase64String(parts[2]);

        if (hashIterations != iterations)
            return false;

        using var algorithm = new Rfc2898DeriveBytes(
            password,
            salt,
            iterations,
            HashAlgorithmName.SHA256);
        var keyToCheck = algorithm.GetBytes(keySize);

        return keyToCheck.SequenceEqual(key);
    }

}
