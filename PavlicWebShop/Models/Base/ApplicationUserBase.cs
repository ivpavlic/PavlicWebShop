﻿namespace PavlicWebShop.Models.Base
{
    public class ApplicationUserBase
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime BirthDate { get; set; }
        //public string? ProductPhoto { get; set; }
        public string? UserImgUrl { get; set; }
    }
}
