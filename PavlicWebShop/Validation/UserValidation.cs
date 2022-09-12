using PavlicWebShop.Services.Interface;
using System.ComponentModel.DataAnnotations;

namespace PavlicWebShop.Validation
{
    public class UserValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)

        {
            var validationService = (IValidationService)validationContext.GetService(typeof(IValidationService));


            if (value is string)
            {
                string email = (string)value;
                if (!validationService.EmailExists(email).Result)
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult("Email already exist!");

            }

            return new ValidationResult("Email is not valid!");

        }
    }
}
