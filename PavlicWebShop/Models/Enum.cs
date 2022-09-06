﻿namespace PavlicWebShop.Models
{
    public static class Roles
    {
        //public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string BasicUser = "BasicUser";
        public const string Employee = "Employee";
    }

    public enum ShoppingCartStatus
    {
        Pending,
        Succeeded,
        Suspended

    }

}
