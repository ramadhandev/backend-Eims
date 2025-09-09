using EIMS.Constants;
using System.ComponentModel.DataAnnotations;

namespace EIMS.Attributes
{
    public class ValidRoleAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return ValidationResult.Success;
            }

            if (value is string role && RoleConstants.AllRoles.Contains(role))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"Invalid role. Must be: {string.Join(", ", RoleConstants.AllRoles)}");
        }
    }
}