﻿using JwtStore.Core.Contexts.AccountContext.ValueObjects;
using JwtStore.Core.Contexts.SharedContext.Extensions;
using JwtStore.Core.Contexts.SharedContext.ValueObjects;
using System.Text.RegularExpressions;

namespace JwtStore.Core.AccountContext.ValueObjects;

public partial class Email : ValueObject
{
    private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

    protected Email()
    {
    }

    public Email(string address)
    {
        if (string.IsNullOrEmpty(address))
            throw new Exception("Email inválido");

        Address = address.Trim().ToLower();

        if (address.Length < 5)
            throw new Exception("Email inválido");

        if (!EmailRegex().IsMatch(Address))
            throw new Exception("Email inválido");
    }

    public string Address { get; }
    public string Hash => Address.ToBase64(); //Gravatar.com
    public Verification Verification { get; private set; } = new();

    public void ResendVerification()
        =>Verification = new Verification();

    public static implicit operator string(Email email)
        => email.ToString();

    public static implicit operator Email(string address)
        => new Email(address);

    public override string ToString()
        => Address;

    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();
}
