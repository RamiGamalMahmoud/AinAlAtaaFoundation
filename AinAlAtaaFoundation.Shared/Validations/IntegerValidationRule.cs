using System.Globalization;
using System.Windows.Controls;

namespace AinAlAtaaFoundation.Shared.Validations
{
    public class IntegerValidationRule : ValidationRule
    {
        public override System.Windows.Controls.ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(int.TryParse(value as string, out int _))
            {
                return System.Windows.Controls.ValidationResult.ValidResult;
            }
            return new System.Windows.Controls.ValidationResult(false, "إدخال غير صالح !");
        }
    }
}
