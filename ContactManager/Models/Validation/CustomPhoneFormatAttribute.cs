using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContactManager.Models
{
    public class CustomPhoneFormatAttribute : ValidationAttribute
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
                isCorrectFormat = Regex.IsMatch(stringToCheck, "^" + Regex.Escape("(") + "[0-9]{3}" + Regex.Escape(")") + Regex.Escape("-") + "[0-9]{3}" + Regex.Escape("-") + "[0-9]{4}$");
                if (isCorrectFormat)
                {
                    return ValidationResult.Success;
                }
            }
            string message = base.ErrorMessage ?? $"{validationContext.DisplayName} must be in the (123)-456-7890 format.";
            return new ValidationResult(message);
        }
    }
}
