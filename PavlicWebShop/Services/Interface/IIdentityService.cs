﻿using PavlicWebShop.Models.Dbo;

namespace PavlicWebShop.Services.Interface
{
    public interface IIdentityService
    {
        Task CreateRoleAsync(string role);
        Task CreateUserAsync(ApplicationUser user, string password, string role);
    }
}