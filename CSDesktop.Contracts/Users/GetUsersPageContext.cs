﻿namespace CSDesktop.Contracts.Users;

public class GetUsersPageContext : GetPageContext
{
    public RoleType Role { get; set; }
}