﻿using PavlicWebShop.Validation;

namespace PavlicWebShop.Models.Binding
{
    public class UserBinding : UserBaseBinding
    {
        
    }

    public class UserBaseBinding
    {
        public string RoleId { get; set; }
        //Validirati empty
        public string Firstname { get; set; }
        //Validirati empty
        public string Lastname { get; set; }
        public AdressBinding UserAdress { get; set; }
        //Validirati DatumRodenja min 18g
        public DateTime BirthDate { get; set; }
        [UserValidation]
        public string Email { get; set; }
        //[StringGreaterThanThanSeven]
        public string Password { get; set; }
        public IFormFile UserImg { get; set; }

    }

    public class UserAdminBinding : UserBaseBinding
    {
        public bool IsEmailConfirmed { get; set; }
    }

    public class UserAdminUpdateBinding : UserAdminBinding
    {
        public string Id { get; set; }
    }

}