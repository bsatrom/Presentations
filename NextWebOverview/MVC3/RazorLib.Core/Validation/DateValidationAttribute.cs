using System;
using System.ComponentModel.DataAnnotations;

namespace RazorLib.Core.Validation
{
    public class DateAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date;
            bool isDate = DateTime.TryParse(value.ToString(), out date);

            if (!isDate || date >= DateTime.Now.Date)
                return ValidationResult.Success;

            return new ValidationResult("The entered date cannot be earlier than the current date");
        }
    }
}