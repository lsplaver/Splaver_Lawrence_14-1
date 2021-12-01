using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContactManager.Models
{
    public class CustomEmailFormatAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return ValidationResult.Success;
            }
            if (value is string)
            {
                string stringToCheck = value.ToString();
                bool isCorrectFormat = false;
                isCorrectFormat = Regex.IsMatch(stringToCheck, "^[a-zA-Z0-9!-_$&.]*" + Regex.Escape("@") + "[a-zA-Z0-9!-_$&.]*" + Regex.Escape(".") + "[a-zA-Z0-9!-_$&.]*$");
                if (isCorrectFormat)
                {
                    return ValidationResult.Success;
                }
            }
            string message = base.ErrorMessage ?? $"{validationContext.DisplayName} must be in the format of 'name@example.com'";
            return new ValidationResult(message);
        }
    }
}
