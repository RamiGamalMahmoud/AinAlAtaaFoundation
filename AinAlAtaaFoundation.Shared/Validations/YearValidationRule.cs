using System;
using System.Globalization;
using System.Windows.Controls;

namespace AinAlAtaaFoundation.Shared.Validations
{
    public class YearValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(int.TryParse(value as string, out int year) && year >= 1900 && year <= DateTime.Now.Year)
            {
                return System.Windows.Controls.ValidationResult.ValidResult;
            }
            return new System.Windows.Controls.ValidationResult(false, "تأكد من كتابة السنة بشكل صحيح");
        }
    }
}
