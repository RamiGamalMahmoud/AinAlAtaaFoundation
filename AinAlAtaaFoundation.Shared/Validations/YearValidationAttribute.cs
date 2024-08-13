using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Shared.Validations
{
    public class YearValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is int year && year >=1940 && year <= DateTime.Now.Year)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(null);
        }
    }
}
