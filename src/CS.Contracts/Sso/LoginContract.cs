﻿namespace CS.Contracts.Sso;

public class LoginContract
{
    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public bool IsAdmin { get; } = true;
}
